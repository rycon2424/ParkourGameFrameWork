using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(this.gameObject, 10);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Horse"))
        {
            other.GetComponentInParent<HorseMovement>().health -= damage ;
            Destroy(this.gameObject);
        }
    }

}
