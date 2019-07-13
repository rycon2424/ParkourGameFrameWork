using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    public float speed;
    private float normalSpeed;

    public Transform player;

	public GameObject[] waypoints;
    
	int currentWaypoint;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<PlayerInput>().transform;
		currentWaypoint = 0;
        normalSpeed = speed;
	}
	
	// Update is called once per frame
	void Update () {

        SpeedOnDistanceCheck();

		transform.position = Vector3.MoveTowards (transform.position, waypoints[currentWaypoint].transform.position, speed * Time.deltaTime);
        
        Quaternion targetRotation = Quaternion.LookRotation(waypoints[currentWaypoint].transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);

		if (currentWaypoint < waypoints.Length - 1) {

			if (Vector3.Distance (transform.position, waypoints [currentWaypoint].transform.position) < 0.1f)
			{
				currentWaypoint++;
			}

		}
		
	}

    void SpeedOnDistanceCheck()
    {
        if (Vector3.Distance(transform.position, player.position) > 40)
        {
            Debug.Log("Player is too Far");
            speed = normalSpeed / 2;
        }
        else if (Vector3.Distance(transform.position, player.position) < 15)
        {
            Debug.Log("Player is too Close");
            speed = speed * 1.3f;
        }
        else
        {
            speed = normalSpeed;
        }
    }

}
