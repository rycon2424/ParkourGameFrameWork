using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waypoint : MonoBehaviour {

    public float speed;
    private float normalSpeed;

    public Transform player;
    public Transform projectile;
    public Transform shootLoc1;
    public Transform shootLoc2;
    public float timeBeforeShoot;

    public GameObject[] waypoints;
    
	int currentWaypoint;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<PlayerInput>().transform;
		currentWaypoint = 0;
        normalSpeed = speed;
        StartCoroutine(Shoot());
	}

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(timeBeforeShoot);
        for (; ; )
        {
            yield return new WaitForSeconds(Random.Range(2.0f, 8.0f));
            int chance = Random.Range(1, 3);
            if (chance == 1)
            {
                Instantiate(projectile, shootLoc1.position, shootLoc1.rotation);
            }
            else
            {
                Instantiate(projectile, shootLoc2.position, shootLoc2.rotation);
            }
        }
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
            if (countingDown == false)
            {
                countingDown = true;
                timerUI.SetActive(true);
                StartCoroutine("CountingTillGameOver");
            }

            speed = normalSpeed / 1.5f;
        }
        else if (Vector3.Distance(transform.position, player.position) < 15)
        {
            Debug.Log("Player is too Close");
            speed = normalSpeed * 1.4f;
        }
        else
        {
            if (countingDown == true)
            {
                countingDown = false;
                timerUI.SetActive(false);
                StopCoroutine("CountingTillGameOver");
            }
            speed = normalSpeed;
        }
    }

    [Header("UI")]
    public GameObject timerUI;
    public Text titel;
    public Text timerText;
    private bool countingDown;
    private int timer;
    private int fulltime;

    IEnumerator CountingTillGameOver()
    {
        titel.text = "You're too far from the target!";
        timer = 15;
        fulltime = timer;
        for (int i = 0; i < fulltime; i++)
        {
            timerText.text = timer.ToString();
            yield return new WaitForSeconds(1f);
            timer--;
        }
        GAME_OVER.gameOver = true;
    }

}
