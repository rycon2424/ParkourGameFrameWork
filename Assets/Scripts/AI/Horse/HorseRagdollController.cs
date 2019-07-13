using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseRagdollController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody[] rigidBodies;
    private Rigidbody rb;
    private bool isDead = false;

    public HorseMovement horse;
    public Collider mainCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        horse = GetComponent<HorseMovement>();
        DisableRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        if (horse.health <= 0 && isDead == false)
        {
            isDead = true;
            EnableRagdoll();
        }
    }

    public void DisableRagdoll()
    {
        horse.anim.enabled = true;
        mainCollider.enabled = true;
        foreach (Rigidbody rb in rigidBodies)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    public void EnableRagdoll()
    {
        horse.anim.enabled = false;
        rb.useGravity = false;
        mainCollider.enabled = false;
        horse.rideAble = false;
        horse.forcedRiding = false;

        foreach (Rigidbody rb in rigidBodies)
        {
            horse.ExitHorse();  // So physics doesn't bamboozle

            rb.isKinematic = false;
            rb.useGravity = true;
            rb.gameObject.GetComponent<Collider>().enabled = true;
            //rb.velocity = Vector3.zero;
        }
        Invoke("Death", 7);
    }

    void Death()
    {
        foreach (Rigidbody rb in rigidBodies)
        {
            rb.isKinematic = true;
        }
    }

}
