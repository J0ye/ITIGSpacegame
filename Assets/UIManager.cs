using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void WriteToScore(string value)
    {
        if(scoreText != null)
        {
            scoreText.text = value;
        }
        else
        {
            Debug.LogWarning("No score text found");
        }
    }
}
