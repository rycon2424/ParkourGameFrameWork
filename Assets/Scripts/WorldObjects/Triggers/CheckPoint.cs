using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    public static Vector3 checkpoint; //SingleTon

    public Transform player;
    public AudioSource checkpointSound;

    [SerializeField]
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (checkpoint.x == 0 && checkpoint.y == 0 && checkpoint.z == 0)
        {
            Debug.Log("No CheckPoints Yet");
        }
        else
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
            Debug.Log("CHECKPOINT");
            anim.Play("CheckPoint");
            //checkpointSound.Play();
            checkpoint = this.transform.position;
        }
    }

}
