using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

    public void Interact()
    {
        if (transform.position == startPos)
        {
            transform.position += Vector3.up;
        }
        else
        {
            transform.position = startPos;
        }
    }
}
