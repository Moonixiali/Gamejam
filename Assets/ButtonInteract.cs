using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteract : MonoBehaviour
{
    public bool active = false;
    public GameObject door;
    public DoorScript doorScript;
    public SpriteRenderer sprite;
    public LineRenderer line;
    void Start()
    {
        doorScript = door.GetComponent<DoorScript>();
        sprite = gameObject.GetComponent<SpriteRenderer>();

        line.SetPosition(0, (gameObject.transform.position + new Vector3(0, 0, -1)));
        line.SetPosition(1, (door.transform.position + new Vector3(0, 0, -1)));
    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
            sprite.color = Color.green;
            line.SetColors(Color.green, Color.green);
            return;
        }
        else {
            sprite.color = Color.red;
            line.SetColors(Color.red, Color.red);
        }
    }
}
