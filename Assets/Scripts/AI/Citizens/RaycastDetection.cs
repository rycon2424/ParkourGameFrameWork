using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDetection : MonoBehaviour
{

    public float debugRange;
    RaycastHit hit;

    void Start()
    {
        
    }
    
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * debugRange, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out hit, debugRange))
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }

}
