using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    public float speed;

	public GameObject[] waypoints = new GameObject[3];
    
	int currentWaypoint;

	// Use this for initialization
	void Start () {

		currentWaypoint = 0;
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = Vector3.MoveTowards (transform.position, 
			waypoints[currentWaypoint].transform.position, speed * Time.deltaTime);


        Quaternion targetRotation = Quaternion.LookRotation(waypoints[currentWaypoint].transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);

		if (currentWaypoint < waypoints.Length - 1) {

			if (Vector3.Distance (transform.position, waypoints [currentWaypoint].transform.position) < 0.1f)
			{
				currentWaypoint++;
			}

		}
		
	}
}
