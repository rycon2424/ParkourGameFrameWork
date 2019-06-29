using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    private PlayerController pc;
    private PlayerInput pi;
    private Animator anim;
    private Transform player;

    public bool playerLocked = false;
    public float playerOffset;

    void Start()
    {
        anim = GameObject.FindObjectOfType<PlayerInput>().gameObject.GetComponent<Animator>();
        player = GameObject.FindObjectOfType<PlayerController>().transform;
        pc = GameObject.FindObjectOfType<PlayerController>();
        pi = GameObject.FindObjectOfType<PlayerInput>();
    }

    void Update()
    {
        if (playerLocked)
        {
            CheckingRaycast();
        }
        if (Input.GetKeyDown(pi.action) && playerLocked)
        {
            Debug.Log("ExitBox");
            transform.parent = null;
            anim.SetBool("PushBox", false);
            pc.enabled = true;
            playerLocked = false;
        }
    }

    RaycastHit hitForward;
    public float distanceForward;
    RaycastHit hitBack;
    public float distanceBack;

    void CheckingRaycast()
    {
        anim.SetFloat("Push Speed", Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.W))
        {
            Debug.DrawRay(new Vector3(player.transform.position.x, player.transform.position.y + 0.3f, player.transform.position.z), player.transform.forward * distanceForward, Color.blue);
            if (Physics.Raycast(new Vector3(player.transform.position.x, player.transform.position.y + 0.3f, player.transform.position.z), player.transform.forward, out hitForward, distanceForward))
            {
                if (hitForward.collider.tag != "")
                {
                    Debug.Log("Blocked");
                    anim.applyRootMotion = false;
                    return;
                }
            }
            anim.applyRootMotion = true;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            Debug.DrawRay(new Vector3(player.transform.position.x, player.transform.position.y + 0.3f, player.transform.position.z), -player.transform.forward * distanceBack, Color.magenta);
            if (Physics.Raycast(new Vector3(player.transform.position.x, player.transform.position.y + 0.3f, player.transform.position.z), -player.transform.forward, out hitBack, distanceBack))
            {
                if (hitBack.collider.tag != "")
                {
                    Debug.Log("Blocked");
                    anim.applyRootMotion = false;
                    return;
                }
            }
            anim.applyRootMotion = true;
        }
    }
    
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(pi.action) && !playerLocked && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                Debug.Log("EnterBox");
                pc.RotateToTarget(transform.position);
                transform.parent = GameObject.FindObjectOfType<PlayerController>().transform;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.23f);
                anim.SetFloat("Speed", 0);
                anim.SetBool("PushBox", true);
                pc.enabled = false;
                Invoke("LockPlayer", 0.5f);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {

    }

    void LockPlayer()
    {
        playerLocked = true;
    }
}
