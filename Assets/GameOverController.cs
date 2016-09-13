using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

    private StoreDataController storeData;
    private Text coinsText;

	// Use this for initialization
	void Start () {
        if (GameObject.FindGameObjectWithTag("StoreData") != null)
            storeData = GameObject.FindGameObjectWithTag("StoreData").GetComponent<StoreDataController>();
        coinsText = GameObject.Find("Coins").GetComponent<Text>();
        UpdateCoins();
	}

    private void UpdateCoins()
    {
        if (storeData != null)
            coinsText.text = "Coins: " + storeData.coins;
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
