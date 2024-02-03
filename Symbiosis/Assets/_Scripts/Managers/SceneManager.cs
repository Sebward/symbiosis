using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class SceneManage : MonoBehaviour
{
    public GameObject menu;
    public GameObject settings;
    public int i_activeScene = 0;
    public int i_menuState = 0;
    public bool b_audioActive;
    // Start is called before the first frame update
    void Start()
    {
        b_audioActive = true;
        i_menuState = 0;
        menu.SetActive(true);
        settings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        i_activeScene = SceneManager.GetActiveScene().buildIndex;
    }
    public void SetScene(int targetScene)
    {
        if (i_activeScene == 0)
        {
            SceneManager.LoadScene(targetScene);
        }
    }

    public void Quit(string s_currentEnviorment)
    {
        if (s_currentEnviorment == "E")
        {
            EditorApplication.ExitPlaymode();
        }
        if (s_currentEnviorment == "B")
        {
            Application.Quit();
        }
    }

    public void back()
    {
        if (i_menuState == 1)
        {
            menu.SetActive(true);
            settings.SetActive(false);
            i_menuState = 0;
        }
    }

    public void Settings()
    {
        if (i_menuState == 0)
        {
            menu.SetActive(false);
            settings.SetActive(true);
            i_menuState = 1;
        }
    }
    public void disableAudio()
    {
        if (b_audioActive)
        {
            AudioListener.volume = 0.0f;
            b_audioActive = false;
            Debug.Log("Audio was disabled by the disableAudio() function within the EventSystem SceneManage Script");
        }
        else if (!b_audioActive)
        {
            AudioListener.volume = 1.0f;
            b_audioActive = true;
            Debug.Log("Audio was enabled by the disableAudio() function within the EventSystem SceneManage Script");
        }
    }
}
