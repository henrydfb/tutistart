using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {

    public GameObject enemy_prefab;
    public GameObject bat_prefab;
    public GameObject enemy_undefined_prefab;
    Enemy enemy;
    float timer = 0f;
    public float spawn_frequency;
    public int wave_size;
    int enemy_count = 0;
    int enemy_killed = 0;
    GameObject enemy_count_display;
    public float spawn_area_up;
    public float spawn_area_down;

    // Use this for initialization
    void Start () {
        enemy_count_display = GameObject.Find("EnemyCountDisplay");
        enemy_count_display.GetComponent<Text>().text = "Enemy killed : 0 / " + wave_size;
    }
	
	// Update is called once per frame
	void Update () {
        if (enemy_count < wave_size)
        {
            timer += Time.deltaTime;
            if (timer >= spawn_frequency)
            {
                Vector3 point = new Vector3(Random.Range(0f, Screen.width), Random.Range(Screen.height * spawn_area_down, Screen.height * spawn_area_up), 0);
                Camera camera = Camera.main;
                Vector3 p = camera.ScreenToWorldPoint(point);
                p.z = 0;
                createEnemy(p);
                timer = 0f;
                enemy_count += 1;
            }
        }
    }

    public void increaseEnemyKilled()
    {
        enemy_killed += 1;
        enemy_count_display.GetComponent<Text>().text = "Enemy killed : " + enemy_killed + " / " + wave_size;
    }

    public void createEnemy(Vector3 p)
    {
        int id = Random.Range(0, 2);
        switch (id)
        {
            case 1: enemy = ((GameObject)Instantiate(bat_prefab)).GetComponent<Bat>(); break;
            case 0: enemy = ((GameObject)Instantiate(enemy_undefined_prefab)).GetComponent<EnemyUndefined>(); break;
        }

        enemy.GetComponent<Transform>().position = p;
    }
}
