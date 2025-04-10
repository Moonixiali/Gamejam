using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            audioManager.PlayOneshotSFX(audioManager.jumpClip);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            audioManager.PlayOneshotSFX(audioManager.deathClip);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            audioManager.PlayOneshotSFX(audioManager.respawnClip);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            audioManager.PlayOneshotSFX(audioManager.doorClip);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            audioManager.PlayOneshotSFX(audioManager.wallSwitchClip);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            audioManager.PlayOneshotSFX(audioManager.floorSwitchClip);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            audioManager.repeatingSFXSource.clip = audioManager.boxScrapeClip;
            audioManager.repeatingSFXSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            audioManager.repeatingSFXSource.clip = audioManager.climbP1;
            audioManager.repeatingSFXSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            audioManager.StopRepeatingSFX();
        }
    }
}
