using UnityEngine;
using System.Collections;

public class PlatformsController : MonoBehaviour {
    public GameObject basePlatform;
    public GameObject currentPlatform;
    public GameObject nextPlatform;

    private Vector3 _startPos;
    private Vector3 _endPos;
    private bool _isMoving;
    private float _timeStartedMoving;

    // Use this for initialization
    void Start () {
        CreateNextPlatform();
    }
	
	// Update is called once per frame
	void Update () {
        //currentPlatform.transform.position = Vector3.Lerp(currentPlatform.transform.position, new Vector3(-Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f)).x + currentPlatform.transform.localScale.x / 2, currentPlatform.transform.position.y, currentPlatform.transform.position.z), smooth * Time.deltaTime);                                        
        //float x = currentPlatform.transform.position.x + (Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f)).x - currentPlatform.transform.localScale.x / 2);
        //if (Input.GetKeyDown(KeyCode.Z)) {
        //    Destroy(oldPlatform);
        //    currentPlatform.transform.position = new Vector3(-Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f)).x + currentPlatform.transform.localScale.x / 2, currentPlatform.transform.position.y, currentPlatform.transform.position.z);
        //bool moving = true;
        //while (moving) {

        //}
        //mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + 1f, mainCamera.transform.position.y, mainCamera.transform.position.z);
        //if (mainCamera.transform.position.x == (currentPlatform.transform.position.x + (Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f)).x - currentPlatform.transform.localScale.x / 2))) {
        //    moving = false;
        //}
        //Debug.Log(currentPlatform.transform.position.x);
        //Debug.Log(currentPlatform.transform.localScale);
        //mainCamera.transform.position = Vector3.Lerp(transform.position, new Vector3(currentPlatform.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z), smooth * Time.deltaTime);            
        //}
    }    

    void FixedUpdate() {
        if (_isMoving) {
            float timeSinceStarted = Time.time - _timeStartedMoving;
            float percentageComplete = timeSinceStarted / 1f;
            nextPlatform.transform.position = Vector3.Lerp(_startPos, _endPos, percentageComplete);
            if (percentageComplete >= 1.0f) {
                _isMoving = false;
                currentPlatform = nextPlatform;
                CreateNextPlatform();
            }
        }
    }
    public void TransitionPlatforms() {
        Destroy(currentPlatform);        
        _isMoving = true;
        _timeStartedMoving = Time.time;
        _startPos = nextPlatform.transform.position;
        _endPos = new Vector3(-Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f)).x + currentPlatform.transform.localScale.x / 2, currentPlatform.transform.position.y, currentPlatform.transform.position.z);        
        //nextPlatform.transform.position = new Vector3(-Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f)).x + nextPlatform.transform.localScale.x / 2, nextPlatform.transform.position.y, nextPlatform.transform.position.z);
    }

    private void CreateNextPlatform() {
        float scale = Random.Range(0.3f, 2.5f);
        float spawnPoint = Random.Range(0, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f)).x - scale / 2);
        nextPlatform = Instantiate(basePlatform, new Vector3(spawnPoint, currentPlatform.transform.position.y, currentPlatform.transform.position.z), Quaternion.identity) as GameObject;
        nextPlatform.transform.localScale = new Vector3(scale, currentPlatform.transform.localScale.y, currentPlatform.transform.localScale.z);
    }
}
