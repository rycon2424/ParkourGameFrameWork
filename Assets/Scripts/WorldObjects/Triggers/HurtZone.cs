using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtZone : MonoBehaviour
{
    public int amount = 1;

    private PlayerStats stats;
    private HorseMovement hm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stats = other.gameObject.GetComponent<PlayerStats>();
        }
        if (other.CompareTag("Horse"))
        {
            hm = other.gameObject.GetComponentInParent<HorseMovement>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stats.Health -= amount;
        }
        if (other.CompareTag("Horse"))
        {
            hm.health -= 50;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
    }
}
