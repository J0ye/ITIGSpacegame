using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public string mainSceneName = "";
    public UnityEvent onAwake = new UnityEvent();
    public TMP_InputField field;

    private void Awake()
    {
        onAwake.Invoke();
        Time.timeScale = 0.1f;
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(mainSceneName);
    }

    public void ChangeMainScene(string newScene)
    {
        mainSceneName = newScene;
    }
}
