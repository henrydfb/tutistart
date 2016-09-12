using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int points;
    public float speed;
    public float angle;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
    }

    protected bool checkIfOutOfScreen()
    {
        Camera camera = Camera.main;
        if (gameObject.GetComponent<Transform>().position.y <= camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y)
            return true;
        else
            return false;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //Destroy(coll.gameObject);
        Destroy(gameObject);
        GameObject.Find("Player").GetComponent<PlayerShooter>().increaseScore(points);
        GameObject.Find("EnemyManager").GetComponent<EnemyManager>().increaseEnemyKilled();
    }
}
