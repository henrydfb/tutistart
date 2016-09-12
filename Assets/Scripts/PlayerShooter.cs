using UnityEngine;
using System.Collections;

public class PlayerShooter : MonoBehaviour {

    public GameObject projectile_prefab;
    GameObject projectile;
    Controls controller;

	// Use this for initialization
	void Start () {
        controller = GameObject.FindWithTag("GameController").GetComponent<Controls>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (controller.isClicked()) {
            createProjectile();
        }
	}

    public void createProjectile() {
        projectile = Instantiate(projectile_prefab, transform.position, Quaternion.identity) as GameObject;
    }
}
