using UnityEngine;
using System.Collections;

public class StairsController : MonoBehaviour {
    public GameObject stair;
    public PlayerController player;
    public PlatformsController platformsController;
    public float rotationTime = 3f;
    public float smooth = 1.5f;

    private GameObject firstStair;
    private GameObject stairsCollection;
    private float stairHeight;
    private Vector3 lastStairPosition;          
    private bool playerCanMove = false;
    
    // Use this for initialization
    void Start() {
        stairHeight = stair.transform.localScale.y;        
        lastStairPosition = new Vector3(platformsController.currentPlatform.transform.position.x + platformsController.currentPlatform.transform.localScale.x / 2f, platformsController.currentPlatform.transform.position.y + platformsController.currentPlatform.transform.localScale.y / 2f + stair.transform.localScale.y/2, 0f);
        stairsCollection = new GameObject("StairsCollection");
        stairsCollection.transform.position = lastStairPosition;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetKey(KeyCode.A) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary) {
            if (!firstStair) {
                GameObject fs = Instantiate(stair, lastStairPosition, Quaternion.identity) as GameObject;
                firstStair = fs;
                fs.transform.parent = stairsCollection.transform;
            } else {
                GameObject obj = Instantiate(stair, new Vector3(lastStairPosition.x, lastStairPosition.y + stairHeight, 0f), Quaternion.identity) as GameObject;
                lastStairPosition = obj.transform.position;
                obj.transform.parent = stairsCollection.transform;
            }
        }

    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.A) || Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)) {
            StartCoroutine(RotateStairs());            
        }
        Debug.Log(playerCanMove);
        if (playerCanMove) {
            player.Move();
        }
    }

    public IEnumerator RotateStairs() {        
        while (stairsCollection.transform.localEulerAngles.z > 270 || stairsCollection.transform.localEulerAngles.z == 0) {
            stairsCollection.transform.rotation = Quaternion.SlerpUnclamped(stairsCollection.transform.rotation, Quaternion.Euler(0, 0, -90), Time.deltaTime);            
            yield return new WaitForEndOfFrame();
        }
        playerCanMove = true;
        yield return 0;
        //float elapsedTime = 0;
        //while (elapsedTime < rotationTime) {
        //    stairsCollection.transform.rotation = Quaternion.Slerp(stairsCollection.transform.rotation, Quaternion.Euler(0, 0, -90), (elapsedTime / rotationTime));
        //    elapsedTime += Time.deltaTime;
        //    Debug.Log(stairsCollection.transform.localEulerAngles);
        //    yield return new WaitForEndOfFrame();
        //}
        //playerCanMove = true;
        //yield return 0;
    }
}
