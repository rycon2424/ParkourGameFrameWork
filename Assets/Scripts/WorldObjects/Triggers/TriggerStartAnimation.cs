using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStartAnimation : MonoBehaviour
{

    public string animationName;
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.Play(animationName);
        }
    }

}
