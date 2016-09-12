using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClearScene : MonoBehaviour {

    bool gameOver = false;

	// Use this for initialization
	void Start () {
        ShooterData data = GameObject.FindGameObjectWithTag("ShooterData").GetComponent<ShooterData>();

        if (data)
        {
            gameOver = data.gameOver;
            GameObject.Find("EnemyKilledInfo").GetComponent<Text>().text = data.enemy_killed + " / " + data.wave_size;
            GameObject.Find("PlayerLivesInfo").GetComponent<Text>().text = "" + data.life;
            GameObject.Find("PlayerScoreInfo").GetComponent<Text>().text = "" + data.score;
            GameObject.Find("BestComboInfo").GetComponent<Text>().text = "" + data.max_combo;
            GameObject.Find("PlayerCorpsesInfo").GetComponent<Text>().text = "" + data.corpses;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void clickOnButton()
    {
        if (gameOver)
            SceneManager.LoadScene("Main_Menu");
        else
            SceneManager.LoadScene("Dig");
    }
}
