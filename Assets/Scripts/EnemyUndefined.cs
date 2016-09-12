using UnityEngine;
using System.Collections;

public class EnemyUndefined : Enemy {

    float sine = 0f;
    int direction = 1;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        sine += direction * Time.deltaTime;

        if (sine >= 1f || sine <= -1f)
            direction *= -1;

        gameObject.GetComponent<Transform>().position += new Vector3(-sine * angle * Time.deltaTime, -speed * Time.deltaTime, 0f);
        if (checkIfOutOfScreen())
        {
            GameObject.Find("Player").GetComponent<PlayerShooter>().decreaseLife(1);
            Destroy(gameObject);
        }
    }
}
