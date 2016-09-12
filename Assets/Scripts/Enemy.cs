using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject death_animation_prefab;

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

        GameObject death_animation = Instantiate(death_animation_prefab) as GameObject;
        death_animation.transform.position = transform.position;
        Destroy(gameObject);
        GameObject.Find("Player").GetComponent<PlayerShooter>().increaseScore(points);
        GameObject.Find("EnemyManager").GetComponent<EnemyManager>().increaseEnemyKilled();
        GameObject.Find("EnemyManager").GetComponent<EnemyManager>().decreaseEnemyOnScreen();
    }
}
