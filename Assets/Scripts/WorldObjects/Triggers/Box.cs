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
    public float distance;

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
            anim.SetFloat("Push Speed", Input.GetAxis("Vertical"));
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
    
    RaycastHit hit;

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(pi.action) && !playerLocked && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                /*if (Physics.Raycast(player.transform.position, (transform.position - player.transform.position).normalized * distance, out hit))
                {
                    Debug.DrawRay(player.transform.position, (transform.position - player.transform.position).normalized * distance, Color.yellow);
                    if (hit.collider.CompareTag("Box"))
                    {*/
                        Debug.Log("EnterBox");
                        pc.RotateToTarget(transform.position);
                        //Debug.Log(hit.normal);
                        //pc.RotateToTarget(hit.normal);
                        transform.parent = GameObject.FindObjectOfType<PlayerController>().transform;
                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.23f);
                        anim.SetFloat("Speed", 0);
                        anim.SetBool("PushBox", true);
                        pc.enabled = false;
                        Invoke("LockPlayer", 0.5f);
                    //}
                //}
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {

        }
    }

    void LockPlayer()
    {
        playerLocked = true;
    }
}
