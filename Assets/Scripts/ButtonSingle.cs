using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonSingle : MonoBehaviour
{
    public bool active;
    public int activeCols;
    public BoxCollider2D collision;
    public BoxCollider2D doorCollider;
    public GameObject door;
    public SpriteRenderer doorSprite;
    public SpriteRenderer sprite;

    void Start() {
        active = false;
        sprite = gameObject.GetComponent<SpriteRenderer>();
        collision = gameObject.GetComponent<BoxCollider2D>();
        
        doorCollider = door.GetComponent<BoxCollider2D>();
        doorSprite = door.GetComponent<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("col enter");
        if (collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Player")) {
            activeCols++;
            sprite.color = Color.green;
            active = true;

            doorCollider.enabled = false;
            doorSprite.color = new Color(1, 1, 1, 0.3f);
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        Debug.Log("col exit");
        if ((collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Player"))) {
            activeCols--;
            if (activeCols == 0) {
                sprite.color = Color.red;
                active = false;

                doorCollider.enabled = true;
                doorSprite.color = new Color(1, 1, 1, 1);
            }
        }
    }
}
