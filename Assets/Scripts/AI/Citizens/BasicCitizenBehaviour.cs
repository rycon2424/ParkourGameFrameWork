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
    public int minTime;
    public int maxTime;
    public bool hasDestination = false;
    int lastDestination;
    

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
        if (act == Acting.walking)
        {
            StartCoroutine(RandomMovement()); // STARTS WALKING
        }
    }
    
    void Update()
    {
        switch (act)
        {
            case Acting.walking:
                if (hasDestination)
                {
                    if (Vector3.Distance(transform.position, DestinationPoints[selectedDestination].transform.position) < 1)
                    {
                        hasDestination = false;
                        thisAgent.enabled = false;
                        //Debug.Log("Destination Reached");
                        anim.SetBool("isIdle", true);
                        anim.SetBool("isWalking", false);
                        StartCoroutine(RandomMovement());
                    }
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
        //Debug.Log("Using Coroutine");

        //Time the AI has to think
        int randomTime = Random.Range(minTime, maxTime);

        //Debug.Log("thinking time = " + randomTime);

        yield return new WaitForSeconds(randomTime);

        //Decides if he wants to idle or walk
        int chance = Random.Range(1, 3);

        switch (chance)
        {
            case 1: // Idle
                //Debug.Log("Idling..");
                StartCoroutine(RandomMovement());
                break;
            case 2: // Walk
                //Debug.Log("Moving..");
                thisAgent.enabled = true;

                //Sets the current destination as last so he cant go to the same destination twice
                lastDestination = selectedDestination;
                //Chooses a random destination
                selectedDestination = Random.Range(0, DestinationPoints.Count);

                //Checks if the selected destination is the last one or already in use, if so it will randomly choose another destination till
                //the destination is not the last and not in use
                while (selectedDestination == lastDestination || DestinationPoints[selectedDestination].GetComponent<DestinationPointScript>().isUsed)
                {
                    selectedDestination = Random.Range(0, DestinationPoints.Count);
                }

                //goes into the destination script and sets the bool to true so other AI cant get there
                DestinationPoints[lastDestination].GetComponent<DestinationPointScript>().isUsed = false;

                //goes into the destination script and sets the bool to true so other AI cant get there
                thisAgent.SetDestination(DestinationPoints[selectedDestination].transform.position);

                //Debug
                //Debug.Log("Last destination = " + lastDestination);
                //Debug.Log("Going to destination = " + DestinationPoints[selectedDestination]);

                //goes into the destination script and sets the bool to true so other AI cant get there
                DestinationPoints[selectedDestination].GetComponent<DestinationPointScript>().isUsed = true;

                hasDestination = true;

                //Sets animations
                anim.SetBool("isWalking", true);
                anim.SetBool("isIdle", false);
                
                break;
        }
    }
}
