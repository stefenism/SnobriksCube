using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public AudioClip jump;
    public AudioClip keyPickup;
    public AudioClip landing;
    public AudioClip melting;
    public AudioClip wooshDown;
    public AudioClip wooshUp;

    public AudioSource sfxSource;

    void Awake()
    {
        sfxSource = GetComponent<AudioSource>();
    }

    public void playJump()
    {
        sfxSource.PlayOneShot(jump);
    }

    public void playKeySound()
    {
        sfxSource.PlayOneShot(keyPickup);
    }
    public void playMelting()
    {
        sfxSource.PlayOneShot(melting);
    }

    
}
