using UnityEngine;
using System.Collections;

public class ShooterDataCreator : MonoBehaviour {

    public GameObject shooter_data_prefab;

	// Use this for initialization
	void Start () {
	    if (!GameObject.FindGameObjectWithTag("ShooterData"))
        {
            Instantiate(shooter_data_prefab).GetComponent<ShooterData>();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
