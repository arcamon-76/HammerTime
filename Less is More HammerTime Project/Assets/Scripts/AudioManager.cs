using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public static AudioManager instance { get; private set; }
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        instance = this;
    }

}
