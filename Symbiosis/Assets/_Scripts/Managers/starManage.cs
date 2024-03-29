using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class starManage : MonoBehaviour
{
    public GameObject go_HUD;
    public GameObject go_PauseMenu;
    public int i_screenState;
    private bool b_isPaused = false;
    void Start()
    {
        i_screenState = 0;
        go_HUD.SetActive(true);
        go_PauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (i_screenState == 0)
            {
                OpenPauseMenue();

            }
            else if (i_screenState == 1)
            {
                ClosePauseMenue();
            }
        }
    }

    public void OpenPauseMenue()
    {
        TogglePause();
        go_HUD.SetActive(false);
        go_PauseMenu.SetActive(true);
        i_screenState = 1;
    }

    public void ClosePauseMenue()
    {
        TogglePause();
        go_PauseMenu.SetActive(false);
        go_HUD.SetActive(true);
        i_screenState = 0;
    }
    public void Quit()
    {
        Application.Quit();

    }
    public void TogglePause()
    {
        b_isPaused = !b_isPaused;

        Time.timeScale = b_isPaused ? 0.0f : 1.0f;

        Time.fixedDeltaTime = b_isPaused ? 0f : 0.02f;
    }
}
