using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public enum GridElementType
    {
        Empty,
        Rock,
        Body
    }

    public GameObject bodyPrefab;
    public GameObject rockPrefab;
    public float gridSeparationI;
    public float gridSeparationJ;

    public int GRID_SIZE_I;
    public int GRID_SIZE_J;
    public int ROCKS_NUM;
    public int BODIES_NUM;
    public int INITIAL_TIME; //Seconds

    public Vector3 gridStartPos;
    public GridElementType[][] grid;
    public GameObject[][] bodiesGrid;
    public GameObject storeDataPrefab;

    private PlayerController player;
    private bool instantiating;
    private float lastBodiesRow;
    private float prevBodiesRow;
    private Text timerText;
    private Text bodiesText;
    private Text levelText;
    private float time;
    private int bodies;
    private StoreDataController storeData;
    
	// Use this for initialization
	void Start () 
    {
        GameObject storeDataObj;

        storeDataObj = GameObject.FindGameObjectWithTag("StoreData");
        if (storeDataObj == null)
            storeDataObj = Instantiate(storeDataPrefab);

        storeData = storeDataObj.GetComponent<StoreDataController>();
        InitLevel();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.indexI = 0;
        player.indexJ = 0;
        player.transform.position = new Vector3(gridStartPos.x + gridSeparationI * player.indexI,gridStartPos.y,player.transform.position.z);
        timerText = GameObject.Find("Time").GetComponent<Text>();
        bodiesText = GameObject.Find("Bodies").GetComponent<Text>();
        levelText = GameObject.Find("Level").GetComponent<Text>();
        time = INITIAL_TIME;
        bodies = 0;
        UpdateTimer();
        grid = new GridElementType[GRID_SIZE_I][];
        bodiesGrid = new GameObject[GRID_SIZE_I][];
        lastBodiesRow = -gridSeparationJ;
        player.indexJ = -1;
        InstantiateBodies();

        levelText.text = "level " + storeData.level;
	}

    public void GoToUpgrade()
    {
        SceneManager.LoadScene("Upgrade");
    }

    private void InitLevel()
    {
        //Level
        //First level
        if (storeData.firstLevel)
        {
            storeData.level = 1;
            storeData.MAX_BODIES = BODIES_NUM;
            storeData.MAX_ROCKS = ROCKS_NUM;
            storeData.TIME = INITIAL_TIME;
            storeData.firstLevel = false;
        }
        else
        {
            storeData.level++;
            BODIES_NUM = storeData.MAX_BODIES;
            ROCKS_NUM = storeData.MAX_ROCKS;
            INITIAL_TIME = storeData.TIME;
        }

        storeData.coins = 500;
    }

    public void UpdateTimer()
    { 
        int min, sec;

        min = (int)(time / 60);
        sec = (int)(time % 60);
        timerText.text = min.ToString("00") + ":" + sec.ToString("00");
    }

    public void UpdateBodies()
    {
        bodiesText.text = "x " + bodies.ToString("000");
    }

    struct EmptySpace
    {
        public int i;
        public int j;
    };

    public void InstantiateBodies()
    {
        int ran, rocks, bodies;
        List<EmptySpace> emptySpaces;
        GameObject body;
        EmptySpace space;

        rocks = ROCKS_NUM;
        bodies = BODIES_NUM;
        gridStartPos = new Vector3(gridStartPos.x, lastBodiesRow);
        emptySpaces = new List<EmptySpace>();
        for (int i = 0; i < GRID_SIZE_I; i++)
        {
            if (grid[i] == null)
                grid[i] = new GridElementType[GRID_SIZE_J];

            if (bodiesGrid[i] == null)
                bodiesGrid[i] = new GameObject[GRID_SIZE_J];

            for (int j = 0; j < GRID_SIZE_J; j++)
            {
               grid[i][j] = GridElementType.Empty;

               if (j == GRID_SIZE_J - 1)
                   lastBodiesRow = gridStartPos .y - j * gridSeparationJ;
               space = new EmptySpace();
               space.i = i;
               space.j = j;
               if(j != 0)
                    emptySpaces.Add(space);
            }
        }

        //Bodies
        for (int i = 0; i < BODIES_NUM; i++)
        {
            ran = Random.Range(0, emptySpaces.Count - 1);

            body = (GameObject)Instantiate(bodyPrefab, gridStartPos + new Vector3(emptySpaces[ran].i * gridSeparationI, -emptySpaces[ran].j * gridSeparationJ), Quaternion.identity);
            body.name = emptySpaces[ran].i.ToString() + emptySpaces[ran].j.ToString();
            grid[emptySpaces[ran].i][emptySpaces[ran].j] = GridElementType.Body;
            bodiesGrid[emptySpaces[ran].i][emptySpaces[ran].j] = body;

            emptySpaces.RemoveAt(ran);
        }

        //Rocks
        for (int i = 0; i < ROCKS_NUM; i++)
        {
            ran = Random.Range(0, emptySpaces.Count - 1);

            Instantiate(rockPrefab, gridStartPos + new Vector3(emptySpaces[ran].i * gridSeparationI, -emptySpaces[ran].j * gridSeparationJ), Quaternion.identity);
            grid[emptySpaces[ran].i][emptySpaces[ran].j] = GridElementType.Rock;

            emptySpaces.RemoveAt(ran);
        }
    }

    public void AddBody()
    {
        bodies++;
        UpdateBodies();
    }

    // Update is called once per frame
    void Update()
    {
        if(instantiating)
        {
            if (player.transform.position.y < prevBodiesRow - gridSeparationJ)
            {
                Debug.Log("FALSE!");
                instantiating = false;
            }
        }
        else
        {
            if (player.transform.position.y <= lastBodiesRow + gridSeparationJ / 2 && player.transform.position.y >= lastBodiesRow - gridSeparationJ / 2)
            {
                instantiating = true;
                prevBodiesRow = lastBodiesRow;
                player.indexJ = 0;
                InstantiateBodies();
            }
        }

        //Timer
        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = 0;
            //Pass data to DigSummary
            storeData.bodies = bodies;
            //Next Part of the game
            if(bodies > 0)
                SceneManager.LoadScene("DigSummary");
            else
                SceneManager.LoadScene("GameOver");
        }

        UpdateTimer();
	}
}
