using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    private Animator playerAnim;
    private PlayerInput playerInput;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }
    
    void Update()
    {
        if (playerAnim.GetBool("isClimbing") == true)
        {
            if (Input.GetKey(playerInput.sprint) && playerAnim.GetFloat("Right") != 0)
            {
                playerAnim.SetBool("FasterClimb", true);
            }
            else
            {
                playerAnim.SetBool("FasterClimb", false);
            }
        }
        else
        {
            playerAnim.SetBool("FasterClimb", false);
        }
    }

}
