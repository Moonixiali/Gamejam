using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public int buttonsActive;
    [SerializeField] private int buttons;

    public BoxCollider2D doorCollider;
    public SpriteRenderer doorSprite;
    void Start()
    {
        doorCollider = GetComponent<BoxCollider2D>();
        doorSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonsActive != buttons) { doorCollider.enabled = true; doorSprite.color = new Color(1, 1, 1, 1); }
        else { doorCollider.enabled = false; doorSprite.color = new Color(1, 1, 1, 0.3f); }
    }
}
