using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{

    public Transform target;
    public Transform barrel;
    public GameObject Bullet;
    public bool activated;
    public int raycastRange;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 25)
        {
            activated = true;
        }
        else
        {
            activated = false;
        }
        if (activated)
        {
            LookAtPlayer();
            Shoot();
        }
    }

    void LookAtPlayer()
    {
        var lookPos = target.position - transform.position;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1);
        Debug.DrawLine(this.transform.position, target.transform.position);
    }

    bool canShoot = true;
    void Shoot()
    {
        if (canShoot)
        {
            StartCoroutine(Shooting());
            canShoot = false;
        }
    }

    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(Bullet, barrel.transform.position, barrel.transform.rotation);
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }
    
}
