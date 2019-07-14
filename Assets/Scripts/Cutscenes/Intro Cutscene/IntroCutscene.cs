using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutscene : MonoBehaviour
{

    [Header("Character animations")]
    public Animator anim;
    public GameObject hud;
    public GameObject maincam;

    public void DisableHUD()
    {
        hud.SetActive(false);
        maincam.SetActive(false);
    }

    public void Walk()
    {
        anim.SetBool("Look", false);
    }

    public void LookAround()
    {
        anim.SetBool("Look", true);
    }

    public void Die()
    {
        anim.SetBool("Look", false);
    }
}
