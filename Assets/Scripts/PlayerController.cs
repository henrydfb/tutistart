using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
    public int indexI;
    public int indexJ;
    public GameObject emptySpacePrefab;

    private const int ROCK_TIME = 3;
    private const int MOVING_STEPS = 5;
    private GameController gameController;
    private GameObject rockFeedback;
    private Vector3 velocity;
    private float speed;
    private bool inRock;
    private float rockTimer;
    private float rockFeedIniSca;
    private bool isMoving;
    private Vector3 movingPos;
    private float movingSpeed;
    
   
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
        Vector2 aim;
        float prevDist;

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

        if (isMoving)
        {
            aim = Aim(new Vector2(transform.position.x, transform.position.y), movingPos);
            prevDist = Vector2.Distance(transform.position, movingPos);
            transform.position += new Vector3(aim.x * movingSpeed, aim.y * movingSpeed, 0);
            //Debug.Log("dis: " + Vector3.Distance(transform.position, movingPos));
            if (Vector2.Distance(transform.position, movingPos) > prevDist)
            {
                transform.position = movingPos;
                isMoving = false;
            }
            GameObject.Find("SandBack").GetComponent<SandBackController>().Emit(transform.position);
            Instantiate(emptySpacePrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }
	}

    public void HandleInput()
    {
        //Mouse
        if (Input.GetButtonDown("Fire1") && !inRock && !isMoving)
        {
            //Right
            if (Input.mousePosition.x > Screen.width / 2)
                MoveRight();
            //Left
            else
                MoveLeft();
        }

        //Left Keyboard
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !inRock && !isMoving)
            MoveLeft();

        //Right Keyboard
        if (Input.GetKeyDown(KeyCode.RightArrow) && !inRock && !isMoving)
            MoveRight();
    }

    private void MoveRight()
    {
        if (indexI + 1 < gameController.GRID_SIZE_I)
        {
            inRock = gameController.grid[indexI + 1][indexJ + 1] == GameController.GridElementType.Rock;
            movingPos = transform.position + new Vector3(speed, -speed);
            indexI++;
            indexJ++;
        }
        else
        {
            inRock = gameController.grid[indexI][indexJ + 1] == GameController.GridElementType.Rock;
            movingPos = transform.position + new Vector3(0, -speed);
            indexJ++;
        }
        isMoving = true;
        movingSpeed = Vector3.Distance(transform.position, movingPos) / MOVING_STEPS;

        //Instantiate(emptySpacePrefab, new Vector3(movingPos.x, movingPos.y, 0), Quaternion.identity);
    }

    private void MoveLeft()
    {
        if (indexI - 1 >= 0)
        {
            inRock = gameController.grid[indexI - 1][indexJ + 1] == GameController.GridElementType.Rock;
            movingPos = transform.position + new Vector3(-speed, -speed);
            indexI--;
            indexJ++;
        }
        else
        {
            inRock = gameController.grid[indexI][indexJ + 1] == GameController.GridElementType.Rock;
            movingPos = transform.position + new Vector3(0, -speed);
            indexJ++;
        }

        isMoving = true;
        movingSpeed = Vector3.Distance(transform.position, movingPos) / MOVING_STEPS;
        //Instantiate(emptySpacePrefab, new Vector3(movingPos.x, movingPos.y, 0), Quaternion.identity);
    }

    public static Vector2 Aim(Vector2 initialPoint, Vector2 finalPoint)
    {
        Vector2 direction, aim;
        float mod;

        direction = new Vector2(finalPoint.x - initialPoint.x, finalPoint.y - initialPoint.y);

        mod = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2));

        if (mod == 0)
        {
            mod = Mathf.Sqrt(Mathf.Pow(finalPoint.x, 2) + Mathf.Pow(finalPoint.y, 2));
            finalPoint.x = finalPoint.x / mod;
            finalPoint.y = finalPoint.y / mod;
            aim = finalPoint;
        }
        else
        {
            direction.x = direction.x / mod;
            direction.y = direction.y / mod;
            aim = direction;
        }

        return aim;
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
