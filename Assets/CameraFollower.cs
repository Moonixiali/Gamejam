using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x,
            player.transform.position.y,
            gameObject.transform.position.z);
    }
}
