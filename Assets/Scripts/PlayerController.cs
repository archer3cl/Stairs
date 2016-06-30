using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public PlatformsController platformsController;
    public GameManagerController gmController;    
    private Vector3 _startPos;
    private Vector3 _endPos;

    private bool _isMoving;
    private bool _isInTransition;
    private float _timeStartedMoving;    

    // Use this for initialization
    void Start () {
        transform.position = new Vector3(platformsController.currentPlatform.transform.position.x + platformsController.currentPlatform.transform.localScale.x / 6f, platformsController.currentPlatform.transform.position.y + platformsController.currentPlatform.transform.localScale.y / 2f + transform.localScale.y / 2, transform.position.z);
        _isInTransition = false;        
	}

    void FixedUpdate() {
        if (_isMoving) {
            float timeSinceStarted = Time.time - _timeStartedMoving;
            float percentageComplete = timeSinceStarted / 1f;
            transform.position = Vector3.Lerp(_startPos, _endPos, percentageComplete);            
            if (percentageComplete >= 1.0f) {
                _isMoving = false;
                if (!_isInTransition) {
                    gmController.PlatformTransition();
                }else {
                    _isInTransition = false;
                }
            }
        }
    }

    public void MoveToNextPlatform() {        
        _isMoving = true;
        _timeStartedMoving = Time.time;
        _startPos = transform.position;
        _endPos = new Vector3(platformsController.nextPlatform.transform.position.x,transform.position.y, transform.position.z);
    }

    public void TransitionPlayer() {
        _isMoving = true;
        _isInTransition = true;
        _timeStartedMoving = Time.time;
        _startPos = transform.position;
        //_endPos = new Vector3(platformsController.currentPlatform.transform.position.x, transform.position.y, transform.position.z);
        _endPos = new Vector3(-Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f)).x + platformsController.currentPlatform.transform.localScale.x / 2, transform.position.y, transform.position.z);
    }  
}
