using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public PlayerController playerController;

    void Awake() {
        gameObject.SetActive(false);
    }
    
    public void BackButton() {
        playerController.MenuMethod();
    }

    public void MainMenuButton() {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
