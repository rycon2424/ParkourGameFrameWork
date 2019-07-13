using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismountHorse : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Horse"))
        {
            other.GetComponentInParent<HorseMovement>().ExitHorse();
            other.GetComponentInParent<HorseMovement>().rideAble = false;
        }
    }
}