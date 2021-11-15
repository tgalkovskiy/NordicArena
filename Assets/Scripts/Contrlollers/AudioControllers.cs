using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllers : MonoBehaviour
{
    private AudioSource _source;
    public AudioClip[] _clips;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void PlaySound(int index)
    {
        _source.PlayOneShot(_clips[index]);
    }
}
