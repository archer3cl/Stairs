using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public PlatformsController platformsController;
    public float playerSpeed = 0.5f;
    // Use this for initialization
    void Start () {
        transform.position = new Vector3(platformsController.currentPlatform.transform.position.x + platformsController.currentPlatform.transform.localScale.x / 6f, platformsController.currentPlatform.transform.position.y + platformsController.currentPlatform.transform.localScale.y / 2f + transform.localScale.y / 2, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Move() {
        while (transform.position.x < 4) {
            transform.position = new Vector3(transform.position.x + playerSpeed, transform.position.y, transform.position.z);
        }        
    }
}
