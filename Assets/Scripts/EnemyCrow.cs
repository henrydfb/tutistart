using UnityEngine;
using System.Collections;

public class EnemyCrow : Enemy {

    float sine = 0f;
    int direction = -1;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        sine += direction * Time.deltaTime;

        if (sine >= 1f || sine <= -1f)
        {
            direction *= -1;
        }

        float offset_x = -sine * angle * Time.deltaTime;
        transform.position += new Vector3(offset_x, -speed * Time.deltaTime, 0f);

        Vector3 scale = transform.localScale;
        if (offset_x < 0f)
            scale.x = -1f;
        else
            scale.x = 1f;

        transform.localScale = scale;
        
        if (checkIfOutOfScreen())
        {
            GameObject.Find("Player").GetComponent<PlayerShooter>().decreaseLife(1);
            GameObject.Find("EnemyManager").GetComponent<EnemyManager>().decreaseEnemyOnScreen();
            Destroy(gameObject);
        }
    }
}
