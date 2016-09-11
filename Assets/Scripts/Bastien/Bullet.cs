using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    //float life_time = 1f;

    Vector3 move;

    // Use this for initialization
    void Start()
    {
        move = new Vector3(0, 1f * Time.deltaTime, 0);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Transform>().position += move;
        //life_time -= 1f * Time.deltaTime;
        //if (life_time <= 0f)
        //{
        //    Destroy(gameObject);
        //}
    }
}
