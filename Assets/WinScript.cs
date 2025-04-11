using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WinScript : MonoBehaviour
{
    public GameObject winMenu;
    public PlayerController playerController;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.transform.CompareTag("Player")) {
            playerController.win = true;
            Time.timeScale = 0.0f;
            winMenu.SetActive(true);
        }
    }
}
