using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : StateBase<PlayerController>
{
    private bool isRootMotion = false;  // Used for root motion of step ups
    private bool waitingBool = false;  // avoids early reset of root mtn
    private bool isStairs = false;
    
    public override void OnEnter(PlayerController player)
    {
        Debug.Log("Succesfully entered the Torch State");
        player.Anim.SetBool("Torch", true);
    }

    public override void OnExit(PlayerController player)
    {
        Debug.Log("Succesfully EXITED the Torch State");
        player.Anim.SetBool("Torch", false);
    }

    public override void Update(PlayerController player)
    {
        if (Input.GetKeyDown(player.Inputf.action))
        {
            Debug.Log("Drop Torch");
            player.Anim.SetBool("Torch", false);
            player.StateMachine.GoToState<Locomotion>();
            return;
        }
        /*AnimatorStateInfo animState = player.Anim.GetCurrentAnimatorStateInfo(0);
        AnimatorTransitionInfo transInfo = player.Anim.GetAnimatorTransitionInfo(0);

        if (player.IsMovingAuto)
            return;
        
        if (!player.Grounded && !isRootMotion)
        {
            player.ResetVerticalSpeed();  // Stops player zooming off a ledge
            player.StateMachine.GoToState<InAir>();
            return;
        }
        else if (player.Ground.Tag == "Slope" && !isRootMotion)
        {
            player.StopMoving();
            player.StateMachine.GoToState<Sliding>();
            return;
        }

        if (isStairs = (player.Ground.Distance < 1f && player.Ground.Tag == "Stairs"))
        {
            player.Anim.SetBool("isStairs", true);

            RaycastHit hit;
            if (Physics.Raycast(player.transform.position + player.transform.forward * 0.2f + 0.2f * Vector3.up,
                Vector3.down, out hit, 1f))
            {
                player.Anim.SetFloat("Stairs", hit.point.y < player.transform.position.y ? -1f : 1f, 0.1f, Time.deltaTime);
            }
        }
        else
        {
            player.Anim.SetBool("isStairs", false);
            player.Anim.SetFloat("Stairs", 0f, 0.1f, Time.deltaTime);
        }*/

        float speed = Input.GetKey(player.Inputf.walk) ? player.WalkSpeed : player.RunSpeed;

        player.MoveGrounded(speed);

        if (player.TargetSpeed > 1f && !isRootMotion)
            player.RotateToVelocityGround();
        
    }

}