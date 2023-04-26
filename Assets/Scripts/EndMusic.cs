using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMusic : MonoBehaviour
{
    public static EndMusic Instance { get; private set; }


    public AudioClip winMusic;
    public AudioClip loseMusic;

    private AudioClip bgm;
    
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        Instance = this;
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayWinMusic()
    {
        PlaySound(winMusic);
    }

    public void PlayLoseMusic()
    {
        PlaySound(loseMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
