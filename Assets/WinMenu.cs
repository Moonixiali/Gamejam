using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public PlayerController playerController;

    public void MainMenuButton() {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void NextButton() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
}