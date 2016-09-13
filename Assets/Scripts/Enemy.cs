using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject death_animation_prefab;

    public int points;
    public float speed;
    public float angle;
    protected bool dead_blow = false;

    float death_delay = 0f;

    Vector3 fly_direction;
    GameObject colliding_bullet;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
        if (dead_blow) {
            blowAway();
        }
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

        if (coll == null) {
            return;
        }
        if (coll.gameObject == null)
        {
            return;
        }
        points *= coll.gameObject.GetComponent<Projectile>().victims + 1;
        
        colliding_bullet = coll.gameObject;
        fly_direction = (transform.position - colliding_bullet.transform.position).normalized;
        dead_blow = true;
        GameObject.Find("Player").GetComponent<PlayerShooter>().increaseScore(points);
        GameObject.Find("EnemyManager").GetComponent<EnemyManager>().increaseEnemyKilled();
        GameObject.Find("EnemyManager").GetComponent<EnemyManager>().decreaseEnemyOnScreen();
    }

    void destroyEnemy() {
        GameObject death_animation = Instantiate(death_animation_prefab) as GameObject;
        death_animation.transform.position = transform.position;
        Destroy(gameObject);
    }

    protected void blowAway() {
        transform.Rotate(new Vector3(0f, 0f, 1f), 1200f * Time.deltaTime);

        death_delay += Time.deltaTime;

        transform.position += fly_direction * speed * 2f * Time.deltaTime;

        if (death_delay >= 1f) {
            destroyEnemy();
        }
    }
}
