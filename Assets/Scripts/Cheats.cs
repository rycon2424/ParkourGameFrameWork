using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{

    public Transform[] checkpoints = new Transform[5];

    public Transform player;
    
    void Update()
    {
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
    }
}
