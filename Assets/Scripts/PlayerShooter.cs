using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShooter : MonoBehaviour {

    public GameObject projectile_prefab;
    GameObject projectile;
    Controls controller;
    GameObject score_display;
    LineRenderer aim_assist;

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

        if (aim_assist == null) {
            aim_assist = gameObject.AddComponent<LineRenderer>();
            aim_assist.SetWidth(0.1f, 0.1f);
            aim_assist.material.color = Color.white;
            aim_assist.SetColors(Color.white, Color.white);
            aim_assist.SetPosition(0, transform.position);
            aim_assist.SetPosition(1, new Vector3(0f, 5f, 0f));
            
        }
    }

    public void increaseScore(int new_score)
    {
        score += new_score;
        score_display.GetComponent<Text>().text = "Player Score : " + score;
    }
}
