using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField]
    private bool pull = true;
    [SerializeField]
    private bool usePlayerAnim;
    [SerializeField]
    private bool needsKey = true;
    [SerializeField]
    private int keycode;

    [Header("Doesnt open box collider")]
    public BoxCollider doesntOpenFromThisSide;

    private Inventory iv;

    void Start()
    {
        iv = GameObject.FindObjectOfType<Inventory>();
    }

    public override void Interact(PlayerController player)
    {
        base.Interact(player);

        if (!player.StateMachine.IsInState<Locomotion>())
            return;

        if (needsKey)
        {
            if (iv.inventoryItem.Count.Equals(0))
            {
                iv.Opendoor(false, "");
                Debug.Log("inventory is empty return");
                return;
            }
            for (int i = 0; i < iv.inventoryItem.Count; i++)
            {
                if (iv.inventoryItem[i].GetComponent<Pickup>().keyNumber == keycode)
                {
                    iv.Opendoor(true, iv.inventoryItem[i].GetComponent<Pickup>().nameObject);
                    Debug.Log("Got the right key");
                    if (iv.inventoryItem[i].GetComponent<Pickup>().destroyOnUse == true)
                    {
                        iv.inventoryItem.Remove(iv.inventoryItem[i]);
                    }
                    doesntOpenFromThisSide.enabled = false;
                    StartCoroutine(OpenDoor(player));
                    return;
                }
                else
                {
                    Debug.Log(iv.inventoryItem[i].GetComponent<Pickup>().nameObject + " is not the right key");
                }
            }
        }
        iv.Opendoor(false, "");
        Debug.Log("Player has not the right key");
    }

    public IEnumerator OpenDoor(PlayerController player)
    {
        if (usePlayerAnim)
        {
            Vector3 playerTargetPos = transform.position - transform.right * (pull ? 1f : 0.75f) - transform.forward * 0.4f;

            player.MoveWait(playerTargetPos, Quaternion.LookRotation(transform.forward), player.WalkSpeed, 16f);
            player.Anim.SetTrigger(pull ? "PullDoorLeft" : "PushDoorLeft");
        }

        Trigger();

        while (player.IsMovingAuto)
        {
            yield return null;
        }

        AnimatorStateInfo stateInfo = player.Anim.GetCurrentAnimatorStateInfo(0);
        while (!stateInfo.IsName("RunWalk") && !stateInfo.IsName("Idle"))
        {
            yield return null;
            stateInfo = player.Anim.GetCurrentAnimatorStateInfo(0);
        } 

        // Stops player opening door at new location
        GetComponent<BoxCollider>().enabled = false;
    }

    public override void Trigger()
    {
        base.Trigger();

        GetComponent<Animator>().Play(pull ? "PullOnLeft" : "Push");
    }
}
