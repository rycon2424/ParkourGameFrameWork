using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismountHorse : MonoBehaviour
{
    public bool killHorse;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Horse") && killHorse == false)
        {
            other.GetComponentInParent<HorseMovement>().ExitHorse();
            other.GetComponentInParent<HorseMovement>().forcedRiding = false;
            other.GetComponentInParent<HorseMovement>().rideAble = false;
        }
        if (other.gameObject.CompareTag("Horse") && killHorse == true)
        {
            other.GetComponentInParent<HorseMovement>().health = 0;
        }
    }
}