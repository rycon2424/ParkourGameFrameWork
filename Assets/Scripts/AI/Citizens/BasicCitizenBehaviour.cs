using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicCitizenBehaviour : MonoBehaviour
{

    [Header("Personality/Stats")]
    public List<GameObject> DestinationPoints;
    public enum Acting { walking, talking, idling }
    public Acting act ;
    public enum Personality {girlKnight, maleKnight, soldier, drunk}
    public Personality pers;
    public float remainingDistance;
    public int minTime;
    public int maxTime;

    private Animator anim;
    private NavMeshAgent thisAgent;
    private int selectedDestination;

    void Start()
    {
        anim = GetComponent<Animator>();
        thisAgent = GetComponent<NavMeshAgent>();
        switch (pers)
        {
            case Personality.girlKnight:
                anim.SetBool("girlKnight", true);
                break;
            case Personality.maleKnight:
                anim.SetBool("maleKnight", true);
                break;
            case Personality.soldier:
                anim.SetBool("soldier", true);
                break;
            case Personality.drunk:
                anim.SetBool("drunk", true);
                break;
            default:
                break;
        }
    }
    
    void Update()
    {
        switch (act)
        {
            case Acting.walking:
                if (thisAgent.enabled == true && thisAgent.remainingDistance < remainingDistance)
                {
                    thisAgent.enabled = false;
                    anim.SetBool("isIdle", true);
                    anim.SetBool("isWalking", false);
                    StartCoroutine(RandomMovement());
                }
                break;
            case Acting.talking:
                if (canTalk)
                {
                    canTalk = false;
                    Invoke("Idling", Random.Range(5, 15));
                }
                break;
            case Acting.idling:
                anim.SetBool("isIdle", true);
                break;
            default:
                break;
        }
    }
    bool canTalk = true;
    void Idling()
    {
        anim.SetBool("isTalking", false);
        anim.SetBool("isIdle", true);
        Invoke("CanTalk", Random.Range(2, 6));
    }
    void CanTalk()
    {
        canTalk = true;
        anim.SetBool("isIdle", false);
        anim.SetBool("isTalking", true);
    }

    public IEnumerator RandomMovement()
    {
        int randomTime = Random.Range(minTime, maxTime);

        //Debug.Log("thinking time = " + randomTime);

        yield return new WaitForSeconds(randomTime);

        int chance = Random.Range(1, 3);

        switch (chance)
        {
            case 1:
                //Debug.Log("prr prr braindead..");
                StartCoroutine(RandomMovement());
                break;
            case 2:
                //Debug.Log("Moving..");
                thisAgent.enabled = true;
                int lastDestination = selectedDestination;
                selectedDestination = Random.Range(0, DestinationPoints.Count);

                while (selectedDestination == lastDestination || DestinationPoints[selectedDestination].GetComponent<DestinationPointScript>().isUsed)
                {
                    selectedDestination = Random.Range(0, DestinationPoints.Count);
                }

                DestinationPoints[lastDestination].GetComponent<DestinationPointScript>().isUsed = false;
                thisAgent.SetDestination(DestinationPoints[selectedDestination].transform.position);
                DestinationPoints[selectedDestination].GetComponent<DestinationPointScript>().isUsed = true;

                Debug.Log("Wut?");
                anim.SetBool("isWalking", true);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isIdle", false);

                break;
        }
    }

}
