using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerShooter : MonoBehaviour {

    public GameObject projectile_prefab;
    GameObject projectile;
    Controls controller;
    GameObject score_display;
    GameObject life_display;

    public int score;
    public int life;

    // Use this for initialization
    void Start () {
        controller = GameObject.FindWithTag("GameController").GetComponent<Controls>();
        score_display = GameObject.Find("PlayerScoreDisplay");
        score_display.GetComponent<Text>().text = "Player Score : " + score;
        life_display = GameObject.Find("PlayerLifeDisplay");
        life_display.GetComponent<Text>().text = "Player life : " + life;
    }
	
	// Update is called once per frame
	void Update () {
        if (controller.isClicked())
        {
            createProjectile();
        }

        if (life <= 0)
        {
            SceneManager.LoadScene("Scenes/GameOver");
        }
	}

public void createProjectile() {
        projectile = Instantiate(projectile_prefab, transform.position, Quaternion.identity) as GameObject;
    }

    public void increaseScore(int new_score)
    {
        score += new_score;
        score_display.GetComponent<Text>().text = "Player Score : " + score;
    }

    public void decreaseLife(int life_lose)
    {
        life -= life_lose;
        life_display.GetComponent<Text>().text = "Player life : " + life;
    }
}
