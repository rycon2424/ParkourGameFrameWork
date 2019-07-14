using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceRideHorse : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Horse"))
        {
            other.GetComponentInParent<HorseMovement>().forcedRiding = true;
        }
    }
}
