using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Object References")]
    public InputActionAsset playerInput;
    public PlayerInput playerInputObject;
    public Rigidbody2D rb;
    public BoxCollider2D coll;
    public BoxCollider2D boxHeldColl;
    public LayerMask boxesLayer;

    public LayerMask climbableLayer;

    public LayerMask buttonsLayer;
    public LayerMask wallclimbLayer;
    public LayerMask groundLayer;

    public GameObject boxHolder;
    public Transform boxHeld;
    public GameObject canInteractIndicator;
    public GameObject pauseMenu;

    [Header("Actions Keyboard")]
    public InputAction moveRightActionKb;
    public InputAction moveLeftActionKb;
    public InputAction jumpActionKb;
    public InputAction interactActionKb;
    public InputAction menuActionKb;

    [Header("Gamepad Keyboard")]
    public InputActionReference movementPositiveGp;
    public InputActionReference movementNegativeGp;
    public InputActionReference jumpGp;
    public InputActionReference interactGp;
    public InputActionReference menuGp;

    [Header("Values")]
    public float speed;
    public float jumpForce;
    public int moveState;
    public bool paused = false;
    public bool flipState = false; //false = right; true = left;
    public float flipStateFloat() {if (flipState) {return -1;} else {return 1;}}
    public bool holdingBox = false;
    public bool win = false;

    public bool climbing = false;
    public bool win = false;


    // Update is called once per frame
    void Update() {
        rb.velocity = new Vector2(moveState * speed, rb.velocity.y);

        //flip logic
        if (holdingBox) { boxHeld.position = boxHolder.transform.position; }
        else if (rb.velocity.x > 0) { flipState = false;
            gameObject.transform.localScale = new Vector2(1f, 1f); }
        else if (rb.velocity.x < 0) { flipState = true;
            gameObject.transform.localScale = new Vector2(-1f, 1f);}

        if (rb.bodyType != RigidbodyType2D.Dynamic) { OnDisable(); }
        else { OnEnable(); }

        //interact indicator
        if (CanInteract() || holdingBox) { canInteractIndicator.SetActive(true); }
        else { canInteractIndicator.SetActive(false); }
    }

    public void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        coll = gameObject.GetComponent<BoxCollider2D>();
        boxHeldColl = boxHolder.GetComponent<BoxCollider2D>();

        string rebinds = PlayerPrefs.GetString("rebinds", string.Empty);
        if (string.IsNullOrEmpty(rebinds)){return;}
        playerInputObject.actions.LoadBindingOverridesFromJson(rebinds);
    }

    public void Awake() {
        jumpActionKb = playerInput.FindActionMap("Gameplay").FindAction("Jump");
        moveRightActionKb = playerInput.FindActionMap("Gameplay").FindAction("Movement Right");
        moveLeftActionKb = playerInput.FindActionMap("Gameplay").FindAction("Movement Left");
        interactActionKb = playerInput.FindActionMap("Gameplay").FindAction("Interaction");
        menuActionKb = playerInput.FindActionMap("Gameplay").FindAction("Menu");
        RegisterInputActions();
    }

    void RegisterInputActions(){
        //keyboard inputs
        jumpActionKb.performed -= ctx => Jump(ctx);
        moveRightActionKb.performed -= ctx => MoveRight(ctx);
        moveLeftActionKb.performed -= ctx => MoveLeft(ctx);
        moveRightActionKb.canceled -= ctx => MoveRightCancel(ctx);
        moveLeftActionKb.canceled -= ctx => MoveLeftCancel(ctx);
        interactActionKb.performed -= ctx => Interact(ctx);
        menuActionKb.performed -= ctx => Menu(ctx);

        jumpActionKb.performed += ctx => Jump(ctx);
        moveRightActionKb.performed += ctx => MoveRight(ctx);
        moveLeftActionKb.performed += ctx => MoveLeft(ctx);
        moveRightActionKb.canceled += ctx => MoveRightCancel(ctx);
        moveLeftActionKb.canceled += ctx => MoveLeftCancel(ctx);
        interactActionKb.performed += ctx => Interact(ctx);
        menuActionKb.performed += ctx => Menu(ctx);
    }

    void MoveRight(InputAction.CallbackContext ctx) {
        moveState += 1;
    }

    void MoveLeft(InputAction.CallbackContext ctx) {
        moveState -= 1;
    }

    void MoveRightCancel(InputAction.CallbackContext ctx) {
        moveState -= 1;
    }

    void MoveLeftCancel(InputAction.CallbackContext ctx) {
        moveState += 1;
    }

    void Jump(InputAction.CallbackContext ctx) {
        if (holdingBox) {return;}
        if (!IsGrounded()) {return;}
        rb.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
    }

    public bool CanInteract() {
        RaycastHit2D ray;
        //Debug.Log("Reached box raycast code");
        ray = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, new Vector2(flipStateFloat(), 0f), .1f, boxesLayer);
        if (ray) {return true;}

        //Debug.Log("Reached switch raycast code");
        ray = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, new Vector2(flipStateFloat(), 0f), .1f, buttonsLayer);
        if (ray) {return true;}

        //Debug.Log("Reached wallclimb raycast code");
        ray = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, new Vector2(flipStateFloat(), 0f), .1f, wallclimbLayer);
        if (ray) {return true;}

        return false;
    }

    void Interact(InputAction.CallbackContext ctx) {

        RaycastHit2D ray;

        Debug.Log("Interact pressed in this context");
        if (holdingBox) {
            holdingBox = false;
            boxHeldColl.enabled = false;
            boxHeld.GetComponent<BoxCollider2D>().enabled = true;
            boxHeld.transform.parent = null;
            return;
        }
        
        //Box interaction code
        Debug.Log("Reached box raycast code");
        ray = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, new Vector2(flipStateFloat(), 0f), .1f, boxesLayer);
        
        if (ray) {
            holdingBox = true;
            ray.transform.SetParent(boxHolder.transform, true);
            boxHeld = ray.transform;
            boxHeld.GetComponent<BoxCollider2D>().enabled = false;
            boxHeldColl.enabled = true;
            return;
        }

        //Switch interaction code
        Debug.Log("Reached switch raycast code");
        ray = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, new Vector2(flipStateFloat(), 0f), .1f, buttonsLayer);
        
        if (ray) {
            var buttonInteract = ray.transform.GetComponent<ButtonInteract>();
            buttonInteract.active = !buttonInteract.active;
            if (buttonInteract.active) { buttonInteract.doorScript.buttonsActive++; }
            else {buttonInteract.doorScript.buttonsActive--; }
            return;
        }

        //Wallclimb interaction code
        Debug.Log("Reached wallclimb raycast code");
        ray = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, new Vector2(flipStateFloat(), 0f), .1f, wallclimbLayer);
        
        if (ray) {
            ray.transform.GetComponent<Wallclimb>().Activate(gameObject);
            return;
        }

        RaycastHit2D climbRay = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, new Vector2(flipStateFloat(), 0f), .1f, climbableLayer);

        if(climbRay) {
            climbing = true;
            Debug.Log("Climbing started");
            StartCoroutine(Climbing());
        }

    }

    IEnumerator Climbing()
    {
        new WaitForSeconds(2f);
        Debug.Log("ending climb");
        climbing = false;
        yield return null;
    }

    void Menu(InputAction.CallbackContext ctx) {
        if (win) {return;}
        MenuMethod();
    }

    public void MenuMethod() {
        if (paused == false) {
            Time.timeScale = 0.0f;
            paused = true;
            //open menu
            pauseMenu.SetActive(true);
        }
        else if (paused == true) {
            Time.timeScale = 1.0f;
            paused = false;
            //close menu
            pauseMenu.SetActive(false);
        }
        Debug.Log("menu pressed in current context");
    }

    public void OnEnable()
    {
        jumpActionKb.Enable();
        moveRightActionKb.Enable();
        moveLeftActionKb.Enable();
        interactActionKb.Enable();
        menuActionKb.Enable();
    }

    public void OnDisable()
    {
        jumpActionKb.Disable();
        moveRightActionKb.Disable();
        moveLeftActionKb.Disable();
        interactActionKb.Disable();
        menuActionKb.Disable();
    }

    public bool IsGrounded() {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }
}
