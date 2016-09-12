using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DigSummaryController : MonoBehaviour {

    private StoreDataController storeData;
    private Text bodiesText;


	// Use this for initialization
	void Start () 
    {
        storeData = GameObject.FindGameObjectWithTag("StoreData").GetComponent<StoreDataController>();
        bodiesText = GameObject.Find("BodiesText").GetComponent<Text>();
        bodiesText.text = storeData.bodies.ToString();
	}

    public void GoToShoot()
    {
        SceneManager.LoadScene("ShooterPhase");
    }
}
