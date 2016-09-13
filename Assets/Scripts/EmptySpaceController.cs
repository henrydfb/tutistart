using UnityEngine;
using System.Collections;

public class EmptySpaceController : MonoBehaviour {

    private GameController gameController;
    private PlayerController player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= player.transform.position.y + gameController.gridSeparationJ * 2.5f)
            Destroy(gameObject);
    }
}
