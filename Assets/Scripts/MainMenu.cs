using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    float timer;

	// Use this for initialization
	void Start () {
        timer = 0f;
	}

    // Update is called once per frame
    void Update(){
    }

    public void goToGame()
    {
        AudioSource audio = GameObject.Find("NormalTap").GetComponent<AudioSource>();
        audio.Play();
        while (timer < audio.clip.length)
        {
            timer += Time.deltaTime;
        }
        SceneManager.LoadScene("Scenes/Dig");
    }

    public void goToUpgrade()
    {
        AudioSource audio = GameObject.Find("UpgradeTap").GetComponent<AudioSource>();
        audio.Play();
        while (timer < audio.clip.length)
        {
            timer += Time.deltaTime;
        }
        SceneManager.LoadScene("Upgrade");
    }
}
