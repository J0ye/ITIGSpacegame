using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public TMP_Text score;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void WriteScore(int scoreUpdate)
    {
        score.text = scoreUpdate.ToString();
    }
}
