using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    Controls controller;
    ScreenCoordinates screen;
    Vector3 init_position;
    Vector3 position;
    Vector3 direction;
    Vector3 offset;
    Vector3 mouse_pos_init;
    Vector3 mouse_pos_current;

    Renderer rend;

    ComboUI ui_combo;
    Rigidbody2D coll;

    bool is_shot = false;
    bool has_passed_screen = false;

    public float speed = 1.0f;

    int victims = 0;

	// Use this for initialization
	void Start () {
        controller = GameObject.FindWithTag("GameController").GetComponent<Controls>();
        screen = GameObject.FindWithTag("Screen").GetComponent<ScreenCoordinates>();
        init_position = transform.position;
        mouse_pos_init = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rend = GetComponent<Renderer>();
        is_shot = false;
        ui_combo = GameObject.FindWithTag("ComboUI").GetComponent<ComboUI>();
    }
	
	// Update is called once per frame
	void Update () {
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

    private void aim() {

        mouse_pos_current = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = mouse_pos_current - mouse_pos_init;

        position = init_position + offset;
        transform.position = position;
    }

    private void shoot() {
        direction = (init_position - position).normalized;
        is_shot = true;

        if (coll == null)
        {
            coll = gameObject.AddComponent<Rigidbody2D>();
            coll.gravityScale = 0;
        }
    }

    private void moveProjectile() {
        transform.position = transform.position + direction * speed * Time.deltaTime;

        if (!screen.isObjectOutOfScreen(gameObject)) {
            has_passed_screen = true;
        }
    }

    private void destroyIfInvisible() {
        if (screen.isObjectOutOfScreen(gameObject) && has_passed_screen)
            Destroy(gameObject);

    }

    void OnCollisionEnter2D(Collision2D coll) {

        victims += 1;

        if (victims > 1) {
            ui_combo.showCombo(victims);
        }
    }
}
