using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            Invoke("LoadMainMenu", 5);
        }
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
