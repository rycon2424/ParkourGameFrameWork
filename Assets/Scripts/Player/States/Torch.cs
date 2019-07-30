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
        player.GroundedOnSteps = true;
        player.Anim.SetBool("Torch", true);
        player.UseRootMotion = true;
        isRootMotion = false;
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

        float speed = Input.GetKey(player.Inputf.walk) ? player.WalkSpeed : player.RunSpeed;

        player.MoveGrounded(speed);

        if (player.TargetSpeed > 1f && !isRootMotion)
            player.RotateToVelocityGround();
        
    }

}