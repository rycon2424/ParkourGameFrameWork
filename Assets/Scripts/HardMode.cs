using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardMode : MonoBehaviour
{
    
    public GameObject guard;
    public bool hardmode = false;
    
    void Update()
    {
        if (hardmode == false && Input.GetKeyDown(KeyCode.K))
        {
            guard.SetActive(true);
            hardmode = true;
        }
    }
}