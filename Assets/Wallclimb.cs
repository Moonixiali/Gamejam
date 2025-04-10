using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallclimb : MonoBehaviour
{
    public GameObject point1;
    public GameObject point2;
    public float speed;
    public float distance;
    public GameObject playerA;
    public Rigidbody2D rb;
    public bool active;
    
    public void Activate(GameObject player) {
        rb = player.GetComponent<Rigidbody2D>();
        playerA = player;
        player.transform.position = point1.transform.position;
        rb.bodyType = RigidbodyType2D.Kinematic;
        distance = point2.transform.position.y - point1.transform.position.y;

        active = true;
    }

    public void Update() {
        if (active) {
            playerA.transform.position = new Vector3(
                playerA.transform.position.x,
                playerA.transform.position.y + (speed * Time.deltaTime),
                playerA.transform.position.z
            );
            distance -= speed * Time.deltaTime;

            if (distance <= 0) {
                active = false;
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.AddForce(new Vector2(0, speed), ForceMode2D.Impulse);
            }
        }
    }
}
