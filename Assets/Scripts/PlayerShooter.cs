using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShooter : MonoBehaviour {

    public GameObject projectile_prefab;
    GameObject projectile;
    Controls controller;
    GameObject score_display;

    public int score;
    public int life;

    // Use this for initialization
    void Start () {
        controller = GameObject.FindWithTag("GameController").GetComponent<Controls>();
        score_display = GameObject.Find("PlayerScoreDisplay");
        score_display.GetComponent<Text>().text = "Player Score : " + score;
    }
	
	// Update is called once per frame
	void Update () {
	    if (controller.isClicked()) {
            createProjectile();
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
}
