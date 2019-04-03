using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float BulletSpeed;
    public Rigidbody rb;
    public Transform dirtEffect;
    public Transform concreteEffect;
    public AudioSource audioPlayer;
    public AudioClip[] soundVariants = new AudioClip[5];

    void Start()
    {
        Destroy(this.gameObject, 10);
    }
    
    void Update()
    {
        transform.Translate (Vector3.forward * BulletSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dirt"))
        {
            Instantiate(dirtEffect, transform.position, dirtEffect.rotation);
        }
        if (other.CompareTag("Concrete"))
        {
            Instantiate(concreteEffect, transform.position, concreteEffect.rotation);
        }
        audioPlayer.clip = soundVariants[Random.Range(0, soundVariants.Length)];
        Debug.Log(audioPlayer.clip);
        audioPlayer.Play();
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
    }

}
