using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject spaceShip;
    public bool state = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && state)
        {
            GameObject newShip = Instantiate(spaceShip, spaceShip.transform.position, spaceShip.transform.rotation);
            newShip.GetComponent<SpaceShipController>().gm = this;
            state = false;
        }
    }

    public void EndCycle()
    {
        state = true;
    }
}
