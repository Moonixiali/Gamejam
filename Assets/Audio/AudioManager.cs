using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("-----Audio Sources-----")]
    [SerializeField] AudioSource musicSource;
    public AudioSource repeatingSFXSource;
    [SerializeField] AudioSource oneshotSFXSource;

    [Header("-----Repeating Audio Clips------")]
    public AudioClip musicTrack;
    public AudioClip boxScrapeClip;
    public AudioClip climbP1;

    [Header("-----Oneshot Audio Clips------")]
    public AudioClip jumpClip;
    public AudioClip deathClip;
    public AudioClip respawnClip;
    public AudioClip doorClip;
    public AudioClip wallSwitchClip;
    public AudioClip floorSwitchClip;

    void Start()
    {
        musicSource.clip = musicTrack;
        musicSource.Play();
    }

    public void PlayOneshotSFX(AudioClip clip)
    {
        oneshotSFXSource.PlayOneShot(clip);
    }

    public void StopRepeatingSFX()
    {
        repeatingSFXSource.Stop();
    }
}
