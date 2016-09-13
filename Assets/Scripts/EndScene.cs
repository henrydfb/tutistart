using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour {

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

        if (gameOver)
        {
            GameObject.Find("Title").GetComponent<Text>().text = "Game Over";
            GameObject.Find("ButtonText").GetComponent<Text>().text = "Return to title";
            GameObject.Find("BGMLose").GetComponent<AudioSource>().Play();
        }
        else
            GameObject.Find("BGMClear").GetComponent<AudioSource>().Play();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void clickOnButton()
    {
        if (gameOver)
        {
            playSound("NormalTap");
            SceneManager.LoadScene("Main_Menu");
        }
        else
        {
            playSound("EscapeTap");
            SceneManager.LoadScene("Dig");
        }
    }

    public void playSound(string sound)
    {
        AudioSource audio = GameObject.Find(sound).GetComponent<AudioSource>();
        float timer = 0f;
        audio.Play();
        while (timer < audio.clip.length)
        {
            timer += Time.deltaTime;
        }
    }
}
