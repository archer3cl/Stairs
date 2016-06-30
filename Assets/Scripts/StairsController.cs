using UnityEngine;
using System.Collections;

public class StairsController : MonoBehaviour {
    public GameObject stair;
    public PlayerController player;
    public PlatformsController platformsController;
    public GameManagerController gmController;

    private GameObject _firstStair;
    private GameObject _stairsCollection;
    private float _stairHeight;
    private Vector3 _lastStairPosition;              

    private Quaternion _startRotationPos;
    private Quaternion _endRotationPos;
    
    private bool _isRotating;
    private float _timeStartedRotating;

    // Use this for initialization
    void Start() {
        _stairHeight = stair.transform.localScale.y;        
        _lastStairPosition = new Vector3(platformsController.currentPlatform.transform.position.x + platformsController.currentPlatform.transform.localScale.x / 2f, platformsController.currentPlatform.transform.position.y + platformsController.currentPlatform.transform.localScale.y / 2f + stair.transform.localScale.y/2, 0f);
        _stairsCollection = new GameObject("StairsCollection");
        _stairsCollection.transform.position = _lastStairPosition;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (_isRotating) {
            float timeSinceStarted = Time.time - _timeStartedRotating;
            float percentageComplete = timeSinceStarted / 1f;
            _stairsCollection.transform.rotation = Quaternion.Slerp(_startRotationPos, _endRotationPos, percentageComplete);
            if (percentageComplete >= 1.0f) {
                _isRotating = false;
                player.MoveToNextPlatform();
            }
        }                        
    }

    void Update() {        
        if (Input.GetKey(KeyCode.A) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary) {
            BuildStairs();
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)) {
            RotateStairs();
        }
    }

    public void ResetStairs() {
        Destroy(_stairsCollection);
        _lastStairPosition = new Vector3(platformsController.currentPlatform.transform.position.x + platformsController.currentPlatform.transform.localScale.x / 2f, platformsController.currentPlatform.transform.position.y + platformsController.currentPlatform.transform.localScale.y / 2f + stair.transform.localScale.y / 2, 0f);
        _stairsCollection = new GameObject("StairsCollection");
        _stairsCollection.transform.position = _lastStairPosition;
    }

    private void BuildStairs() {
        if (!_firstStair) {
            GameObject fs = Instantiate(stair, _lastStairPosition, Quaternion.identity) as GameObject;
            _firstStair = fs;
            fs.transform.parent = _stairsCollection.transform;
        } else {
            GameObject obj = Instantiate(stair, new Vector3(_lastStairPosition.x, _lastStairPosition.y + _stairHeight, 0f), Quaternion.identity) as GameObject;
            _lastStairPosition = obj.transform.position;
            obj.transform.parent = _stairsCollection.transform;
        }
    }

    private void RotateStairs() {
        _isRotating = true;        
        _timeStartedRotating = Time.time;        
        _startRotationPos = _stairsCollection.transform.rotation;
        _endRotationPos = Quaternion.Euler(0,0,-90);
    }
}
