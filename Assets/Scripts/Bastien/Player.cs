using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public GameObject bulletPrefab;
    Bullet bullet;
    public int score;
    GameObject score_display;
    public int life;

    // Use this for initialization
    void Start () {
        score_display = GameObject.Find("PlayerScoreDisplay");
        score_display.GetComponent<Text>().text = "Player Score : " + score;
    }
	
	// Update is called once per frame
	void Update () {
            if (Input.GetButtonDown("Fire1"))
            {
                bullet = ((GameObject)Instantiate(bulletPrefab)).GetComponent<Bullet>();
                bullet.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
            }
    }

    public void increaseScore(int new_score)
    {
        score += new_score;
        score_display.GetComponent<Text>().text = "Player Score : " + score;
    }
}
