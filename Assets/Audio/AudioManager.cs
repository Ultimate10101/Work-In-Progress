using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip backgroundMusic;



    public static AudioManager audioManagerRef;

    private void Awake()
    {
        if (audioManagerRef == null)
        {
            audioManagerRef = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
