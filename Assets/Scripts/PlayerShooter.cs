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
    GameObject corpses_display;

    public int score;
    public int life;
    public int corpses;

    // Use this for initialization
    void Start () {
        controller = GameObject.FindWithTag("GameController").GetComponent<Controls>();
        score_display = GameObject.Find("PlayerScoreDisplay");
        score_display.GetComponent<Text>().text = "Player Score : " + score;
        life_display = GameObject.Find("PlayerLifeDisplay");
        life_display.GetComponent<Text>().text = "Player life : " + life;
        corpses_display = GameObject.Find("CorpsesAmountDisplay");
        corpses_display.GetComponent<Text>().text = "Corpses : " + corpses;
    }
	
	// Update is called once per frame
	void Update () {
        if (controller.isClicked() && corpses > 0)
        {
            createProjectile();
            corpses -= 1;
            corpses_display.GetComponent<Text>().text = "Corpses : " + corpses;
        }

        if (life <= 0)
        {
            ShooterData ShooterData = GameObject.FindGameObjectWithTag("ShooterData").GetComponent<ShooterData>();
            ShooterData.gameOver = true;
            SceneManager.LoadScene("Scenes/EndScene");
        }
	}


    void OnDestroy()
    {
        ShooterData ShooterData = GameObject.FindGameObjectWithTag("ShooterData").GetComponent<ShooterData>();
        ShooterData.savePlayer(score, life, corpses);
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
