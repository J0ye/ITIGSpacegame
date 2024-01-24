using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;
public enum NewMenuState { Main, Setting}

public class MainMenuManager : MonoBehaviour
{

    public NewMenuState state = NewMenuState.Main;
    public string mainSceneName = "";
    public UnityEvent onAwake = new UnityEvent();

    [Header ("Settings")]
    public AudioMixer mixer;
    public GameObject main;
    public GameObject settings;
    public TMP_Dropdown dropDown;
    private Resolution[] m_resolution;

    private void Awake()
    {
        onAwake.Invoke();
        Time.timeScale = 0.1f;
    }

    public void Start()
    {
        SetMenuState();

        DropDownStart();
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(mainSceneName);
    }

    public void ChangeMainScene(string newScene)
    {
        mainSceneName = newScene;
    }

    public void SetMenuState()
    {
        switch (state)
        {
            case NewMenuState.Main:
                main.SetActive(true);
                settings.SetActive(false);
                break;
            case NewMenuState.Setting:
                main.SetActive(false);
                settings.SetActive(true);
                break;
        }
    }

    public void SwitchMenuState()
    {
        _ = state == NewMenuState.Setting ? state = NewMenuState.Main : state = NewMenuState.Setting;

        SetMenuState();
    }


    private void DropDownStart()
    {
        m_resolution = Screen.resolutions;
        dropDown.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < m_resolution.Length; i++)
        {
            string s = m_resolution[i].width + "x" + m_resolution[i].height;
            options.Add(s);
        }

        dropDown.AddOptions(options);

        for (int i = 0; i < options.Count; i++)
        {
            if (options[i] == Screen.currentResolution.width.ToString() + "x" + Screen.currentResolution.height.ToString())
            {
                dropDown.value = i;

                dropDown.RefreshShownValue();
            }
        }
    }
}
