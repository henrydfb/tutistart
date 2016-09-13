using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour {

    public GameObject enemy_prefab;
    public GameObject bat_prefab;
    public GameObject crow_prefab;
    Enemy enemy;
    float timer = 0f;
    public float spawn_frequency;
    public int sound_frequency;
    public int wave_size;
    int enemy_count = 0;
    int enemy_killed = 0;
    GameObject enemy_count_display;
    public float spawn_area_up;
    public float spawn_area_down;
    int enemy_on_screen = 0;

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
                Vector3 point = new Vector3(Random.Range(20f, Screen.width - 20f), Random.Range(Screen.height * spawn_area_down, Screen.height * spawn_area_up), 0);
                Camera camera = Camera.main;
                Vector3 p = camera.ScreenToWorldPoint(point);
                p.z = 0;
                createEnemy(p);
                timer = 0f;
                enemy_count += 1;
            }
        }
        else if (enemy_on_screen <= 0)
        {
            ShooterData ShooterData = GameObject.FindGameObjectWithTag("ShooterData").GetComponent<ShooterData>();
            ShooterData.gameOver = false;
            AudioSource audio = GameObject.Find("AllEnemyBastard").GetComponent<AudioSource>();
            float timer = 0f;
            audio.Play();
            while (timer < audio.clip.length)
            {
                timer += Time.deltaTime;
            }
            SceneManager.LoadScene("Scenes/EndScene");
        }
    }

    void OnDestroy()
    {
        ShooterData shooter_data = GameObject.FindGameObjectWithTag("ShooterData").GetComponent<ShooterData>();
        shooter_data.saveEnemyManager(enemy_killed, wave_size);
    }

    public void increaseEnemyKilled()
    {
        enemy_killed += 1;
        enemy_count_display.GetComponent<Text>().text = "Enemy killed : " + enemy_killed + " / " + wave_size;
    }

    public void decreaseEnemyOnScreen()
    {
        enemy_on_screen -= 1;
    }

    public void createEnemy(Vector3 p)
    {
        enemy_on_screen += 1;
        int id = Random.Range(0, 2);
        switch (id)
        {
            case 1: enemy = ((GameObject)Instantiate(bat_prefab)).GetComponent<Bat>(); break;
            case 0: enemy = ((GameObject)Instantiate(crow_prefab)).GetComponent<EnemyCrow>(); break;
        }

        enemy.GetComponent<Transform>().position = p;
    }
}