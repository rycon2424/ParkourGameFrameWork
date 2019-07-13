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
            ExitBox();
        }
        /*if (Input.GetMouseButton(1))
        {
            CheckFace();
        }*/
    }

    RaycastHit hitForward;
    public float distanceForward;
    RaycastHit hitBack;
    public float distanceBack;

    [Header("Test")]
    RaycastHit hitTest;
    public float hitTestRange;

    void CheckFace()
    {
        Vector3 playerHeight = new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z);
        Debug.DrawRay(playerHeight, (transform.position - playerHeight).normalized * hitTestRange, Color.red);
        if (Physics.Raycast(playerHeight, (transform.position - playerHeight).normalized, out hitTest, hitTestRange))
        {
            if (hitTest.collider.tag == "Box")
            {
                player.rotation = Quaternion.LookRotation(-hitTest.normal, Vector3.up);
            }
        }
    }


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
                CheckFace();
                transform.parent = GameObject.FindObjectOfType<PlayerController>().transform;
                //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, playerOffset);
                anim.SetFloat("Speed", 0);
                anim.SetBool("PushBox", true);
                pc.enabled = false;
                gameObject.layer = 2;
                Invoke("LockPlayer", 0.5f);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        ExitBox();
    }

    public void ExitBox()
    {
        if (playerLocked)
        {
            gameObject.layer = 0;
            transform.parent = null;
            anim.SetBool("PushBox", false);
            pc.enabled = true;
            playerLocked = false;
            anim.applyRootMotion = true;
        }
    }

    void LockPlayer()
    {
        playerLocked = true;
    }
}
