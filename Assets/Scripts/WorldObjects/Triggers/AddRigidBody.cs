using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRigidBody : MonoBehaviour
{
    public GameObject[] fallingObject = new GameObject[1];
    //public AudioSource sound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < fallingObject.Length; i++)
            {
                fallingObject[i].AddComponent<Rigidbody>();
            }
            // sound.Play();
            Debug.Log("Break");
            Destroy(this.gameObject);
        }
    }

}
