using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtZone : MonoBehaviour
{
    public int amount = 1;
    public int timeBetweenDamage;

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
        if (other.CompareTag("Player") && canTakeDamage)
        {
            stats.Health -= amount;
            StartCoroutine(Cooldown());
        }
        if (other.CompareTag("Horse") && canTakeDamage)
        {
            hm.health -= 50;
            StartCoroutine(Cooldown());
        }
    }

    bool canTakeDamage = true;
    IEnumerator Cooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(timeBetweenDamage);
        canTakeDamage = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
    }
}
