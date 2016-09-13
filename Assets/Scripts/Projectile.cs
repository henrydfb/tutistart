using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public GameObject aim_assist_prefab;
    GameObject aim_assist;

    Controls controller;
    ScreenCoordinates screen;
    Vector3 init_position;
    Vector3 position;
    Vector3 direction;
    Vector3 offset;
    Vector3 mouse_pos_init;
    Vector3 mouse_pos_current;

    ShooterData shooter_data;

    Renderer rend;

    ComboUI ui_combo;
    Rigidbody2D coll;

    bool is_shot = false;
    bool has_passed_screen = false;

    public float speed = 1.0f;
    public float rotation_speed = 1.0f;

    public int victims = 0;

	// Use this for initialization
	void Start () {
        controller = GameObject.FindWithTag("GameController").GetComponent<Controls>();
        screen = GameObject.FindWithTag("Screen").GetComponent<ScreenCoordinates>();
        init_position = transform.position;
        mouse_pos_init = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rend = GetComponent<Renderer>();
        is_shot = false;
        ui_combo = GameObject.FindWithTag("ComboUI").GetComponent<ComboUI>();

        aim_assist = Instantiate(aim_assist_prefab) as GameObject;
        aim_assist.transform.position = transform.position;

        shooter_data = GameObject.FindGameObjectWithTag("ShooterData").GetComponent<ShooterData>();
    }
	
	// Update is called once per frame
	void Update () {
        direction = (init_position - position).normalized;

        rotate();

        if (controller.hold && !is_shot)
            aim();

        if (controller.isReleased() && !is_shot) {
            shoot();
        }

        if (is_shot) {
            moveProjectile();
        }

        destroyIfInvisible();
	}

    void rotate() {
        transform.Rotate(new Vector3(0f, 0f, 1f), rotation_speed * Time.deltaTime);
    }

    private void aim() {

        mouse_pos_current = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = mouse_pos_current - mouse_pos_init;

        position = init_position + offset;
        transform.position = position;

        aim_assist.transform.position = transform.position;

        if (direction != Vector3.zero)
        {
            aim_assist.transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void shoot() {
        if (direction == Vector3.zero) {
            Destroy(gameObject);
            return;
        }

        is_shot = true;

        if (coll == null)
        {
            coll = gameObject.AddComponent<Rigidbody2D>();
            coll.gravityScale = 0;
        }
    }

    private void moveProjectile() {
        transform.position = transform.position + direction * speed * Time.deltaTime;
        aim_assist.transform.position = transform.position;

        if (!screen.isObjectOutOfScreen(gameObject)) {
            has_passed_screen = true;
        }
    }

    private void destroyIfInvisible() {
        if (screen.isObjectOutOfScreen(gameObject) && has_passed_screen)
        {
            Destroy(aim_assist);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {

        victims += 1;

        if (victims > shooter_data.max_combo)
            shooter_data.max_combo = victims;

        if (victims > 1) {
            ui_combo.showCombo(victims);
        }
    }
}
