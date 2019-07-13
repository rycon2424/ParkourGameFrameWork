using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class HorseMovement : MonoBehaviour
{

    [Header("Horse")]
    public int health;
    public bool rideAble;
    public bool playerRiding;
    public bool sprint;
    public bool forcedRiding;
    public bool canJump;

    [Header("Horse Stats")]
    public float speed;
    public float rotateSpeed;
    public float jumpHeight;
    public GameObject postEffect;

    private bool canGallop = true;
    [HideInInspector]
    public Animator anim;
    private GameObject player;
    private CameraController cc;
    private PlayerInput pi;
    private PlayerStats ps;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        cc = GameObject.FindObjectOfType<CameraController>();
        pi = GameObject.FindObjectOfType<PlayerInput>();
        ps = GameObject.FindObjectOfType<PlayerStats>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (forcedRiding)
        {
            transform.Translate(Vector3.forward * (speed * 6) * Time.deltaTime);
            anim.SetBool("Idle", false);
            anim.SetBool("Run", true);
            anim.SetBool("Walk", false);
            if (Input.GetKeyDown(pi.jump) && canJump)
            {
                Jump();
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.up * (-rotateSpeed) * Time.deltaTime);
                transform.Translate(-transform.right * (speed) * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up * (rotateSpeed) * Time.deltaTime);
                transform.Translate(transform.right * (speed) * Time.deltaTime);
            }
        }
        else if (playerRiding)
        {
            HorseControls();
            if (anim.GetBool("Run") == true)
            {
                postEffect.SetActive(true);
            }
            else
            {
                postEffect.SetActive(false);
            }
            CanDieOnHorse();
        }
        
        if (!canJump)
        {
            CheckGround();
        }
        
    }

    void CanDieOnHorse()
    {
        if (ps.Health <= 0)
        {
            forcedRiding = true;
            Invoke("ExitHorse", 1.5f);
        }
    }

    void HorseControls()
    {
        if (Input.GetKeyDown(pi.action) && canExit)
        {
            ExitHorse();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprint = !sprint;
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (sprint && canGallop)
            {
                transform.Translate(Vector3.forward * (speed * 5) * Time.deltaTime);
                anim.SetBool("Idle", false);
                anim.SetBool("Run", true);
                anim.SetBool("Walk", false);
                if (Input.GetKeyDown(pi.jump) && canJump)
                {
                    Jump();
                }
            }
            else
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                anim.SetBool("Idle", false);
                anim.SetBool("Run", false);
                anim.SetBool("Walk", true);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.up * -rotateSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            }
        }
        else
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Run", false);
            anim.SetBool("Walk", false);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (rideAble == true)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (Input.GetKeyDown(pi.action))
                {
                    canExit = false;
                    EnterHorse();
                    Invoke("CanExit", 1);
                }
            }
            if (other.CompareTag("Restrict Gallop"))
            {
                canGallop = false;
                sprint = false;
            }
        }
    }

    void Jump()
    {
        canJump = false;
        anim.Play("Jump");
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight);
        Invoke("CanCheckForGround", 0.5f);
    }

    void EnterHorse()
    {
        playerRiding = true;
        this.transform.GetChild(5).gameObject.SetActive(false);
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Animator>().SetBool("Riding", true);
        player.GetComponent<Animator>().Play("Riding Idle");
        Vector3 horseVector = new Vector3(transform.position.x, transform.position.y + 0.9f, transform.position.z);
        player.transform.position = horseVector;
        player.transform.parent = transform.GetChild(2);
        player.transform.rotation = transform.rotation;
        //cc.Target = this.transform.GetChild(5);
    }

    public void ExitHorse()
    {
        playerRiding = false;
        this.transform.GetChild(5).gameObject.SetActive(true);
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<Animator>().SetBool("Riding", false);
        player.transform.parent = null;
        anim.SetBool("Idle", true);
        anim.SetBool("Run", false);
        anim.SetBool("Walk", false);
        Invoke("ResetAnim", 1f);
        //cc.Target = player.transform;
    }

    void ResetAnim()
    {
        anim.SetBool("Idle", true);
        anim.SetBool("Run", false);
        anim.SetBool("Walk", false);
    }

    bool canExit = false;
    void CanExit()
    {
        canExit = true;
    }

    RaycastHit hit;
    bool canCheck = false;
    void CanCheckForGround()
    {
        canCheck = true;
    }
    void CheckGround()
    {
        Debug.DrawRay(transform.GetChild(2).position, -transform.up * 1.6f, Color.magenta);
        if (Physics.Raycast(transform.GetChild(2).position, -transform.up, out hit, 1.6f) && canCheck == true)
        {
            if (hit.collider.tag != "")
            {
                Debug.Log("Ground");
                canJump = true;
                canCheck = false;
            }
        }
    }

    public void ShakeCam()
    {
        if (ps.Health <= 0)
        {
            return;
        }
        CameraShaker.Instance.ShakeOnce(1f, 2f, .4f, .4f);
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Restrict Gallop"))
        {
            canGallop = true;
        }
    }

}
