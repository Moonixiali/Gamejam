using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    private Vector3 playerSpawn;
    [SerializeField] GameObject canvas;

    private void Awake()
    {
        playerSpawn = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerKill"))
        {
            transform.position = playerSpawn;
        }
        if (collision.gameObject.CompareTag("PlayerFinishLevel"))
        {
            canvas.SetActive(true);
        }
    }
}
