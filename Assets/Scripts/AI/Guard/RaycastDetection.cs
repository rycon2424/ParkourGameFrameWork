using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDetection : MonoBehaviour
{

    public float debugRange;
    RaycastHit hit;
    DisplayText dt;
    GuardSounds gs;

    void Start()
    {
        dt = GameObject.FindObjectOfType<DisplayText>();
        gs = GameObject.FindObjectOfType<GuardSounds>();
    }
    
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * debugRange, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out hit, debugRange) && GAME_OVER.gameOver == false)
        {
            if (hit.collider.tag == "Player")
            {
                Debug.Log(hit.collider.gameObject.name);
                dt.Thought("You have been caught", false, "");
                gs.Caught();
                gs.caught = true;
                GAME_OVER.gameOver = true;
            }
        }
    }

}
