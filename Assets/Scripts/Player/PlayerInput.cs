using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public KeyCode action = KeyCode.E;
    public KeyCode jump = KeyCode.Space;
    public KeyCode walk = KeyCode.LeftAlt;
    public KeyCode sprint = KeyCode.LeftShift;
    public KeyCode crouch = KeyCode.C;
    public KeyCode drawWeapon = KeyCode.Mouse1;
    public KeyCode spotLight = KeyCode.F;
    public KeyCode inventory = KeyCode.Tab;
    public KeyCode pause = KeyCode.Escape;
}
