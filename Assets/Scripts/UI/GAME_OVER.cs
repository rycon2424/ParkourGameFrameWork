using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GAME_OVER : MonoBehaviour
{

    public static bool gameOver;
    public static bool canPause;
    public bool doOnce;

    public GameObject gameoverCanvas;

    void Start()
    {
        gameOver = false;
        canPause = true;
        doOnce = true;
    }

    void Update()
    {
        if (gameOver && doOnce)
        {
            canPause = false;
            doOnce = false;
            gameoverCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Invoke("FreezeGame", 2.1f);
        }
    }

    public void FreezeGame()
    {
        Time.timeScale = 0;
    }

    public void LastCheckPoint()
    {
        gameOver = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
