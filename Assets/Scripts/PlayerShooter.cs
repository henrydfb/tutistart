using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerShooter : MonoBehaviour {

    public GameObject projectile_prefab;
    GameObject projectile;
    Controls controller;
    GameObject score_display;
    GameObject life_display;
    GameObject corpses_display;

    SpriteRenderer sprite_rend;

    public Sprite aim_sprite;
    public Sprite shoot_sprite;
    private StoreDataController storeData;

    public int score;
    public int life;
    public int corpses;

    public float delay_to_idle = 1f;
    float time = 0f;
    bool has_shot = false;

    // Use this for initialization
    void Start () {
        /*GameObject storeDataObj;

        storeDataObj = GameObject.FindGameObjectWithTag("StoreData");
        storeData = storeDataObj.GetComponent<StoreDataController>();
        corpses = storeData.bodies;*/
        controller = GameObject.FindWithTag("GameController").GetComponent<Controls>();
        score_display = GameObject.Find("PlayerScoreDisplay");
        score_display.GetComponent<Text>().text = "Player Score : " + score;
        life_display = GameObject.Find("PlayerLifeDisplay");
        life_display.GetComponent<Text>().text = "Player life : " + life;
        

        sprite_rend = GetComponent<SpriteRenderer>();

        corpses_display = GameObject.Find("CorpsesAmountDisplay");
        corpses_display.GetComponent<Text>().text = "Corpses : " + corpses;
    }
	
	// Update is called once per frame
	void Update () {

        lookAtCursor();

        if (controller.isReleased())
        {
            GameObject.Find("Throw").GetComponent<AudioSource>().Play();
            sprite_rend.sprite = shoot_sprite;
            has_shot = true;
        }

        if (has_shot)
        {
            time += Time.deltaTime;

            if (time >= delay_to_idle)
            {
                sprite_rend.sprite = aim_sprite;
                time = 0f;
                has_shot = false;
            }
        }

        if (controller.isClicked() && corpses > 0)
        {
            createProjectile();
            sprite_rend.sprite = aim_sprite;
            
            
        }

        corpses_display.GetComponent<Text>().text = "Corpses : " + corpses;

        if (life <= 0 || corpses <= 0)
        {
            ShooterData ShooterData = GameObject.FindGameObjectWithTag("ShooterData").GetComponent<ShooterData>();
            ShooterData.gameOver = true;
            SceneManager.LoadScene("Scenes/EndScene");
        }
	}

    void lookAtCursor() {
            Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 new_scale = transform.localScale;
            if (transform.position.x < mouse_pos.x) {
                new_scale.x = 1.5f;
            } else {
                new_scale.x = -1.5f;
            }
            transform.localScale = new_scale;
    }

    void OnDestroy()
    {
        ShooterData ShooterData = GameObject.FindGameObjectWithTag("ShooterData").GetComponent<ShooterData>();
        ShooterData.savePlayer(score, life, corpses);
    }

    public void createProjectile() {
        projectile = Instantiate(projectile_prefab, transform.position, Quaternion.identity) as GameObject;
    }

    public void increaseScore(int new_score)
    {
        score += new_score;
        score_display.GetComponent<Text>().text = "Player Score : " + score;
    }

    public void decreaseLife(int life_lose)
    {
        life -= life_lose;
        life_display.GetComponent<Text>().text = "Player life : " + life;
    }

    public void decreaseCorpse() {
        corpses -= 1;
    }
}
