using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject mainmenu;
    public GameObject optionsmenu;
    public Slider sensitivity;

    private CameraController cc;
    bool openMenu = false;
    float oldTimeScale;

    void Start()
    {
        cc = GameObject.FindObjectOfType<CameraController>();
        sensitivity.value = cc.rotationSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && openMenu == false)
        {
            openMenu = true;
            mainmenu.SetActive(true);
            optionsmenu.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            oldTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }
        if (openMenu)
        {
            Cursor.visible = true;
        }
    }

    public void Resume()
    {
        mainmenu.SetActive(false);
        optionsmenu.SetActive(false);
        openMenu = false;
        Time.timeScale = oldTimeScale;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Options()
    {
        mainmenu.SetActive(false);
        optionsmenu.SetActive(true);
    }

    public void BackToMain()
    {
        mainmenu.SetActive(true);
        optionsmenu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeSensitivity()
    {
        cc.rotationSpeed = sensitivity.value;
    }
    
}
