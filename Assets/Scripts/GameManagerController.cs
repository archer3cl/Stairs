using UnityEngine;
using System.Collections;

public class GameManagerController : MonoBehaviour {
    public PlatformsController platformsController;
    public PlayerController player;
    public StairsController stairsController;


    private bool _isOnTransition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlatformTransition() {
        _isOnTransition = true;
        platformsController.TransitionPlatforms();        
        player.TransitionPlayer();
        stairsController.ResetStairs();
        _isOnTransition = false;
    }

    public bool IsOnTransition {
        get { return _isOnTransition; }
    }
}
