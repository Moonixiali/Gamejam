using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteract : MonoBehaviour
{
    public bool active = false;
    public GameObject door;
    public BoxCollider2D doorCollider;
    public SpriteRenderer doorSprite;
    public SpriteRenderer sprite;
    void Start()
    {
        doorCollider = door.GetComponent<BoxCollider2D>();
        doorSprite = door.GetComponent<SpriteRenderer>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
            sprite.color = Color.green;
            doorCollider.enabled = false;
            doorSprite.color = new Color(1, 1, 1, 0.3f);
            return;
        }
        else {
            sprite.color = Color.red;
            doorCollider.enabled = true;
            doorSprite.color = new Color(1, 1, 1, 1);
        }
    }
}
