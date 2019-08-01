using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoesntOpen : MonoBehaviour
{
    private Inventory iv;
    private PlayerInput pi;

    public bool showmessage;

    void Start()
    {
        iv = GameObject.FindObjectOfType<Inventory>();
        pi = GameObject.FindObjectOfType<PlayerInput>();
    }

    void Update()
    {
        if (Input.GetKeyDown(pi.action) && showmessage)
        {
            iv.DoesNotOpenFromThisSide();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            showmessage = true;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            showmessage = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            showmessage = false;
        }
    }
}
