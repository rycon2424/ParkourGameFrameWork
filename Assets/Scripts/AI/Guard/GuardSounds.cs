using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSounds : MonoBehaviour
{
    public bool caught;

    public AudioSource audioSource;

    public AudioClip alarmAudio;
    public AudioClip[] footsteps;

    public void Footstep()
    {
        if (caught)
        {
            return;
        }
        audioSource.clip = footsteps[Random.Range(0, footsteps.Length)];
        audioSource.Play();
    }

    public void Caught()
    {
        if (caught)
        {
            return;
        }
        audioSource.clip = alarmAudio;
        audioSource.Play();
    }

}
