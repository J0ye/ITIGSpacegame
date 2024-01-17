using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> targets = new List<GameObject>();
    [Range(0f, 5f)]
    public float gameSpeed = 1.0f;
    [Range(1f, 50f)]
    public float waveCycleDuration = 5.0f;
    [Header("Enemies")]
    public List<GameObject> enemyPrefabs = new List<GameObject>();
    public Vector2 rangeForEnemeyPositionsX = Vector2.zero;
    public Vector2 rangeForEnemeyPositionsZ = Vector2.zero;
    public Vector3 enemeySpawnArea = Vector3.zero;
    public int maxEnemyAmmount = 2;
    public int enemyIncreaseEveryWave = 3;
    public bool state = false;
    public bool pause = false;
    [Header("Player")]
    public GameObject spaceShipPrefab;
    public Transform spaceShip;
    public WeaponStats gunStat;
    public WeaponStats laserStat;

    protected PositionList positionList;
    protected float cycleTimer = 0f;
    protected int score = 0;
    protected int waveCounter = 0;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this);
        }

        positionList = new PositionList(1, enemeySpawnArea, rangeForEnemeyPositionsX, rangeForEnemeyPositionsZ);
    }

    protected void Start()
    {
        LogCreator.instance.AddLog("Game starts in " + waveCycleDuration.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && state)
        {
            spaceShip = Instantiate(spaceShipPrefab, spaceShipPrefab.transform.position, spaceShipPrefab.transform.rotation).transform;
            state = false;
        }

        if (Input.GetKeyDown(KeyCode.N) && !pause)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            gameSpeed = 0.1f;
            pause = true;
        }
        else if(pause && Input.GetKeyDown(KeyCode.N))
        {
            pause = false;
            gameSpeed = 1f;
            SceneManager.UnloadSceneAsync(1);
        }

        if(!pause)
        {
            if (cycleTimer >= waveCycleDuration)
            {
                WaveCycle();
                cycleTimer = 0;
            }
            else
            {
                cycleTimer += Time.deltaTime * gameSpeed;
            }
        }
    }

    public void EndCycle()
    {
        state = true;
    }

    public void AddToScore(int value)
    {
        score += value;
        UiManager.instance.WriteScore(score);
    }
    
    public void SubtractFromScore(int value)
    {
        score -= value;
        UiManager.instance.WriteScore(score);
    }

    public void DamageShip()
    {
        spaceShip.GetComponent<SpaceShipController>().Damage();
    }

    /// <summary>
    /// Is called every WaveCycleDuration seconds
    /// </summary>
    protected void WaveCycle()
    {
        waveCounter++;
        if (waveCounter % enemyIncreaseEveryWave == 0)
        {
            maxEnemyAmmount++;
        }
        for (int i = targets.Count; i < maxEnemyAmmount; i++)
        {
            SpawnEnemy();
        }
    }

    protected void SpawnEnemy()
    {
        int rand = Random.Range(0, enemyPrefabs.Count);
        Vector3 pos = positionList.GetRandomPositionNoPop();

        GameObject newEnemy = Instantiate(enemyPrefabs[rand], pos, Quaternion.identity);
        targets.Add(newEnemy);
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        if(targets.Contains(enemy))
        {
            targets.Remove(enemy);
        }
    }
}
