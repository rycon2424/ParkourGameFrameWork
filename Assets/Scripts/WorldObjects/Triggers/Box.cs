using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    private PlayerController pc;
    private PlayerInput pi;

    void Start()
    {
        pc = GameObject.FindObjectOfType<PlayerController>();
        pi = GameObject.FindObjectOfType<PlayerInput>();
    }

    void Update()
    {

    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("1");
            if (pc.CharControl.enabled == true)
            {
                Debug.Log("2");
                if (Input.GetKeyDown(pi.action))
                {
                    Debug.Log("3");
                    pc.DisableCharControl();
                    pc.RotateToTarget(transform.position);
                }
            }
        }
    }
}
