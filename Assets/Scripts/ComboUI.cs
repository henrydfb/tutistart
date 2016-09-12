using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ComboUI : MonoBehaviour {

    Text display;

    public float delay = 3.0f;

    string label;
    bool active_timer = false;

	// Use this for initialization
	void Start () {
        display = GetComponent<Text>();
        label = display.text;
        display.GetComponent<CanvasRenderer>().SetAlpha(0f);
    }
	
	// Update is called once per frame
	void Update () {
	    if (active_timer) {
            display.CrossFadeAlpha(0f, delay, false);
            active_timer = false;
        }
	}

    public void showCombo(int value) {
        display.text = label + " : " + value;

        display.GetComponent<CanvasRenderer>().SetAlpha(1f);
        active_timer = true;
    }
}
