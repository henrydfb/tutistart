using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ShooterData data = GameObject.FindGameObjectWithTag("ShooterData").GetComponent<ShooterData>();
        print(data);
        if (data)
        {
            GameObject.Find("EnemyKilledInfo").GetComponent<Text>().text = data.enemy_killed + " / " + data.wave_size;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void goToTitleScreen()
    {
        AudioSource audio = GameObject.Find("EscapeTap").GetComponent<AudioSource>();
        float timer = 0f;
        audio.Play();
        while (timer < audio.clip.length)
        {
            timer += Time.deltaTime;
        }
        SceneManager.LoadScene("Main_Menu");
    }
}
