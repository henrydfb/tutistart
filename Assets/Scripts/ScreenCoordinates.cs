using UnityEngine;
using System.Collections;

public class ScreenCoordinates : MonoBehaviour {

    float left;
    float top;
    float right;
    float bottom;

	// Use this for initialization
	void Start () {
        Camera cam = Camera.main;
        Vector3 topleft = cam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 bottomright = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        left = topleft.x;
        top = topleft.y;
        right = bottomright.x;
        bottom = bottomright.y;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool isObjectOutOfScreen(GameObject target) {
        Vector3 position = target.transform.position;

        if (position.x < left || position.x > right || position.y < top || position.y > bottom)
            return true;
        else
            return false;
    }
}
