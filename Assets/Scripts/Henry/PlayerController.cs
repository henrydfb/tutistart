using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
    public int indexI;
    public int indexJ;
    public GameObject emptySpacePrefab;

    private const int ROCK_TIME = 3;
    private GameController gameController;
    private GameObject rockFeedback;
    private Vector3 velocity;
    private float speed;
    private bool inRock;
    private float rockTimer;
    private float rockFeedIniSca;
    

	// Use this for initialization
	void Start () {
        velocity = new Vector3();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rockFeedback = transform.Find("rockFeedback").gameObject;
        rockFeedback.SetActive(false);
        rockFeedIniSca = rockFeedback.transform.localScale.x;
        speed = gameController.gridSeparation;
        rockTimer = 0;
        AdjustSandBack();
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Input Controller
        HandleInput();

        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y , Camera.main.transform.position.z);
        AdjustSandBack();

        if (inRock)
        {
            rockFeedback.SetActive(true);
            if (rockTimer >= ROCK_TIME)
            {
                inRock = false;
                rockTimer = 0;
                rockFeedback.SetActive(false);
            }
            else
            {
                rockFeedback.transform.localScale = new Vector3(rockFeedIniSca - (rockTimer * rockFeedIniSca) / ROCK_TIME, rockFeedback.transform.localScale.y, rockFeedback.transform.localScale.z); 
                rockTimer += Time.deltaTime;
            }
        }
	}

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !inRock)
        {
            if (indexI - 1 >= 0)
            {
                inRock = gameController.grid[indexI - 1][indexJ + 1] == GameController.GridElementType.Rock;

                transform.position += new Vector3(-speed, -speed);
                indexI--;
                indexJ++;
            }
            else
            {
                inRock = gameController.grid[indexI][indexJ + 1] == GameController.GridElementType.Rock;
                transform.position += new Vector3(0, -speed);
                indexJ++;
            }

            Instantiate(emptySpacePrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }


        if (Input.GetKeyDown(KeyCode.RightArrow) && !inRock)
        {
            if (indexI + 1 < gameController.GRID_SIZE_I)
            {
                inRock = gameController.grid[indexI + 1][indexJ + 1] == GameController.GridElementType.Rock;
                transform.position += new Vector3(speed, -speed);
                indexI++;
                indexJ++;
            }
            else
            {
                inRock = gameController.grid[indexI][indexJ + 1] == GameController.GridElementType.Rock;
                transform.position += new Vector3(0, -speed);
                indexJ++;
            }

            Instantiate(emptySpacePrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }
    }

    private void AdjustSandBack()
    {
        GameObject.Find("SandBack").transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 2.3f, GameObject.Find("SandBack").transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Body")
        {
            gameController.AddBody();
            DestroyObject(other.gameObject);
        }
    }
}
