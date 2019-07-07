﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    public static Vector3 checkpoint; //SingleTon

    [SerializeField]
    private Transform player;
    [SerializeField]
    private AudioSource checkpointSound;

    [SerializeField]
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        checkpointSound = GetComponent<AudioSource>();
        player = GameObject.FindObjectOfType<PlayerController>().transform;
        if (checkpoint.x != 0 && checkpoint.y != 0 && checkpoint.z != 0)
        {
            player.transform.position = checkpoint;
        }
        if (checkpoint == this.transform.position)
        {
            anim.Play("CheckPoint");
        }
    }
    
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            anim.Play("CheckPoint");
            //checkpointSound.Play();
            checkpoint = this.transform.position;
        }
    }

}
