using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject mainmenu;
    public GameObject optionsmenu;
    public Slider sensitivity;
    public Slider audioVolume;
    public static float sensitivitySave = 120;

    private CameraController cc;
    bool openMenu = false;
    public static bool blockInventory;
    float oldTimeScale;

    void Start()
    {
        cc = GameObject.FindObjectOfType<CameraController>();
        cc.rotationSpeed = sensitivitySave;
        sensitivity.value = sensitivitySave;
        audioVolume.value = AudioListener.volume;
    }

    void Update()
    {
        blockInventory = openMenu;
        if (GAME_OVER.gameOver == false && GAME_OVER.canPause == true)
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
        sensitivitySave = sensitivity.value;
        cc.rotationSpeed = sensitivitySave;
    }

    public void ChangeAudio()
    {
        Debug.Log("AudioChanged");
        AudioListener.volume = audioVolume.value;
    }

}
