using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject spaceShip;
    public bool state = false;
    public bool pause = false;



    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this);
        }
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

        if (Input.GetKeyDown(KeyCode.N) && !pause)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            pause = true;
        }
        else if(pause && Input.GetKeyDown(KeyCode.N))
        {
            pause = false;
            SceneManager.UnloadSceneAsync(1);
        }
    }

    public void EndCycle()
    {
        state = true;
    }
}
