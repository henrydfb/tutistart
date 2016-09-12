using UnityEngine;
using System.Collections;

public class Bat : Enemy {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Transform>().position += new Vector3(-angle * Time.deltaTime, -speed * Time.deltaTime, 0f);
        if (checkIfOutOfScreen())
        {
            GameObject.Find("Player").GetComponent<PlayerShooter>().decreaseLife(1);
            Destroy(gameObject);
        }
    }
}
