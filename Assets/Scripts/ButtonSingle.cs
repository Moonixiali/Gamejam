using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonSingle : MonoBehaviour
{
    public bool active;
    public int activeCols;
    public BoxCollider2D collision;
    public GameObject door;
    public DoorScript doorScript;
    public SpriteRenderer doorSprite;
    public SpriteRenderer sprite;

    public LineRenderer line;

    void Start() {
        active = false;
        sprite = gameObject.GetComponent<SpriteRenderer>();
        collision = gameObject.GetComponent<BoxCollider2D>();
        doorScript = door.GetComponent<DoorScript>();


        line.SetPosition(0, (gameObject.transform.position + new Vector3(0, 0, -1)));
        line.SetPosition(1, (door.transform.position + new Vector3(0, 0, -1)));

        sprite.color = Color.red;
        line.SetColors(Color.red, Color.red);
    }
    void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("col enter");
        if (collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Player")) {
            activeCols++;
            sprite.color = Color.green;
            line.SetColors(Color.green, Color.green);

            if (!active) { doorScript.buttonsActive++; }
            active = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        Debug.Log("col exit");
        if ((collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Player"))) {
            activeCols--;
            if (activeCols == 0) {
                sprite.color = Color.red;
                line.SetColors(Color.red, Color.red);
                active = false;

                doorScript.buttonsActive--;
            }
        }
    }
}
