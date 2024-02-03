using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public GameObject menu;
    public GameObject settings;
    public int i_buildIndex;
    public int i_currentscreen;
    public bool b_audioState;
    void Start()
    {
        b_audioState = true;
        i_buildIndex = SceneManager.GetActiveScene().buildIndex;
        i_currentscreen = 0;
        menu.SetActive(true);
        settings.SetActive(false); 
    }
    public void SetScene(int SceneIndex)
    {
        if (SceneIndex != i_currentscreen)
        {
            SceneManager.LoadScene(SceneIndex);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Settings()
    {
        if (i_currentscreen == 0)
        {
            menu.SetActive(false);
            settings.SetActive(true);
            i_currentscreen = 1;
        }
    }

    public void Back()
    {
        if (i_currentscreen == 1)
        {
            menu.SetActive(true);
            settings.SetActive(false);
            i_currentscreen = 0;
        }
    }

    public void SetAudio()
    {
        if (b_audioState == false)
        {
            AudioListener.volume = 1.0f;
            b_audioState = true;
        }

        else if (b_audioState == true)
        {
            AudioListener.volume = 0.0f;
            b_audioState = false;
        }
    }
}
