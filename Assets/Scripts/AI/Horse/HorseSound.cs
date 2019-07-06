using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseSound : MonoBehaviour
{

    private AudioSource audioplayer;
    public AudioClip[] horseRun;
    public AudioClip[] horseWalk;

    void Start()
    {
        audioplayer = GetComponent<AudioSource>();
    }

    void HorseRun()
    {
        audioplayer.clip = horseRun[Random.Range(0, horseRun.Length)];
        audioplayer.Play();
    }
    
    void HorseWalk()
    {
        audioplayer.clip = horseWalk[Random.Range(0, horseWalk.Length)];
        audioplayer.Play();
    }

}
