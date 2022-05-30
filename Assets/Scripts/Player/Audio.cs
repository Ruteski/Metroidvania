using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private AudioSource _audioSource;

    public AudioClip coinSound;
    public AudioClip jumSound;
    public AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip sfx) {
        _audioSource.PlayOneShot(sfx);
    }
}
