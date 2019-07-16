using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOnRange : MonoBehaviour
{
    public float range;
    public Transform target;
    public GameObject goToShow;

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < range)
        {
            goToShow.SetActive(true);
        }
        else
        {
            goToShow.SetActive(false);
        }
    }

}
