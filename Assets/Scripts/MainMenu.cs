using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    void Update(){
    }

    public void goToGame()
    {
        SceneManager.LoadScene("Scenes/Dig");
    }

    public void goToUpgrade()
    {
        //SceneManager.LoadScene();
    }
}
