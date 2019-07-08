using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{

    public Transform[] checkpoints = new Transform[5];

    public Transform player;
    public float timeSpeed;
    public GameObject defaultDance;
    
    void Update()
    {
        Time.timeScale = timeSpeed;
        if (Input.GetKey(KeyCode.Keypad1))
        {
            player.position = checkpoints[0].position;
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            player.position = checkpoints[1].position;
        }
        if (Input.GetKey(KeyCode.Keypad3))
        {
            player.position = checkpoints[2].position;
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            player.position = checkpoints[3].position;
        }
        if (Input.GetKey(KeyCode.Keypad5))
        {
            player.position = checkpoints[4].position;
        }
        if (Input.GetKey(KeyCode.Keypad6))
        {
            player.position = checkpoints[5].position;
        }
        if (Input.GetKey(KeyCode.Keypad7))
        {
            player.position = checkpoints[6].position;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Instantiate(defaultDance, new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject.FindObjectOfType<PlayerStats>().Health = 0;
        }
    }
}
