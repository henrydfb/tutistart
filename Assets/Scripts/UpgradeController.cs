using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpgradeController : MonoBehaviour {

    public int INCREASE_TIME;
    public int INCREASE_BODIES;
    public int DECREASE_ROCKS;

    public int TIME_PRICE;
    public int BODIES_PRICE;
    public int ROCKS_PRICE;

    private StoreDataController storeData;
    private Text coinsText;

	// Use this for initialization
	void Start () 
    {
        if(GameObject.FindGameObjectWithTag("StoreData") != null)
            storeData = GameObject.FindGameObjectWithTag("StoreData").GetComponent<StoreDataController>();

        if (storeData != null)
        {
            BODIES_PRICE = storeData.bodyPrice;
            TIME_PRICE = storeData.timePrice;
            ROCKS_PRICE = storeData.rockPrice;
        }

        GameObject.Find("TimeText").GetComponent<Text>().text = "+Time " + TIME_PRICE;
        GameObject.Find("BodyText").GetComponent<Text>().text = "+Body " + BODIES_PRICE;
        GameObject.Find("RockText").GetComponent<Text>().text = "-Rock " + ROCKS_PRICE;
        coinsText = GameObject.Find("Coins").GetComponent<Text>();
        UpdateCoins();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    private void UpdateCoins()
    {
        if(storeData != null)
            coinsText.text = "Coins: " + storeData.coins;
    }

    public void IncreaseTime()
    {
        PlaySound();
        if (storeData != null)
        {
            if (storeData.coins >= TIME_PRICE)
            {
                storeData.TIME += INCREASE_TIME;
                storeData.coins -= TIME_PRICE;
                UpdateCoins();
            }
        }
    }

    public void IncreaseBodies()
    {
        PlaySound();
        if (storeData != null)
        {
            if (storeData.coins >= BODIES_PRICE)
            {
                storeData.MAX_BODIES += INCREASE_BODIES;
                storeData.coins -= BODIES_PRICE;
                UpdateCoins();
            }
        }
    }

    public void DecreaseRocks()
    {
        PlaySound();
        if (storeData != null)
        {
            if (storeData.coins >= ROCKS_PRICE)
            {
                storeData.MAX_ROCKS -= DECREASE_ROCKS;
                storeData.coins -= ROCKS_PRICE;
                UpdateCoins();
            }
        }
    }

    public void GoToMainMenu()
    {
        PlaySound();
        SceneManager.LoadScene("Main_Menu");
    }

    public void PlaySound()
    {
        float timer = 0f;
        AudioSource audio = GameObject.Find("NormalTap").GetComponent<AudioSource>();
        audio.Play();
        while (timer < audio.clip.length)
        {
            timer += Time.deltaTime;
        }
    }
}
