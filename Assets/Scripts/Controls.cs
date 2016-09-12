using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

    Vector3 mouse_position = new Vector3(0.0f, 0.0f);
    public bool hold = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        updateMouse();
	}

    private void updateMouse () {
        mouse_position = Input.mousePosition;

        if (Input.GetButtonDown("Fire1"))
            hold = true;
        else if (Input.GetButtonUp("Fire1"))
            hold = false;
    }

    public bool isClicked() {
        return Input.GetButtonDown("Fire1");
    }

    public bool isReleased() {
        return Input.GetButtonUp("Fire1");
    }
}
