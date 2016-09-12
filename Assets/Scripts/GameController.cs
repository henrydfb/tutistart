using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public enum GridElementType
    {
        Empty,
        Rock,
        Body
    }

    public GameObject bodyPrefab;
    public GameObject rockPrefab;
    public float gridSeparation;

    public int GRID_SIZE_I;
    public int GRID_SIZE_J;
    public Vector3 gridStartPos;
    public GridElementType[][] grid;

    private const int ROCKS_NUM = 3;
    private const int BODIES_NUM = 10;

    private int initialTime = 30; //Seconds
    private PlayerController player;
    private bool instantiating;
    private float lastBodiesRow;
    private float prevBodiesRow;
    private Text timerText;
    private Text bodiesText;
    private float time;
    private int bodies;
    private StoreDataController storeData;
    
	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.indexI = 0;
        player.indexJ = 0;
        player.transform.position = new Vector3(gridStartPos.x + gridSeparation * player.indexI,gridStartPos.y,player.transform.position.z);
        timerText = GameObject.Find("Time").GetComponent<Text>();
        bodiesText = GameObject.Find("Bodies").GetComponent<Text>();
        storeData = GameObject.Find("StoreData").GetComponent<StoreDataController>();
        time = initialTime;
        bodies = 0;
        UpdateTimer();
        grid = new GridElementType[GRID_SIZE_I][];
        InstantiateBodies();
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

    public void InstantiateBodies()
    {
        int ran, rocks, bodies;

        rocks = ROCKS_NUM;
        bodies = BODIES_NUM;
        player.indexJ = -1;
        gridStartPos = new Vector3(gridStartPos.x, (player.transform.position + new Vector3(0, -gridSeparation)).y);
        for (int i = 0; i < GRID_SIZE_I; i++)
        {
            if (grid[i] == null)
                grid[i] = new GridElementType[GRID_SIZE_J];

            for (int j = 0; j < GRID_SIZE_J; j++)
            {
                if (j != 0)
                {
                    ran = Random.Range(0, 100);
                    if (ran > 90 && ran < 95 && bodies > 0)
                    {
                        Instantiate(bodyPrefab, gridStartPos + new Vector3(i * gridSeparation, -j * gridSeparation), Quaternion.identity);
                        grid[i][j] = GridElementType.Body;
                        bodies--;
                    }
                    else if (ran > 95 && rocks > 0)
                    {
                        Instantiate(rockPrefab, gridStartPos + new Vector3(i * gridSeparation, -j * gridSeparation), Quaternion.identity);
                        grid[i][j] = GridElementType.Rock;
                        rocks--;
                    }
                    else
                    { 
                        //Empty
                        grid[i][j] = GridElementType.Empty;
                    }

                }
                else
                {
                    grid[i][j] = GridElementType.Empty;
                }

                if (j == GRID_SIZE_J - 1)
                    lastBodiesRow = gridStartPos .y - j * gridSeparation;
            }
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
            if (player.transform.position.y < prevBodiesRow - gridSeparation)
            {
                Debug.Log("FALSE!");
                instantiating = false;
            }
        }
        else
        {
            if (player.transform.position.y <= lastBodiesRow + gridSeparation / 2 && player.transform.position.y >= lastBodiesRow - gridSeparation / 2)
            {
                instantiating = true;
                prevBodiesRow = lastBodiesRow;
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
            SceneManager.LoadScene("DigSummary");
        }

        UpdateTimer();
	}
}
