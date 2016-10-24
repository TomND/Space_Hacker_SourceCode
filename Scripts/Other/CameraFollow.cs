using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class CameraFollow : MonoBehaviour {

    public GameObject cameraObject;
    public GameObject player;
    public float speed;
    Vector3 relativePosition;
    public float headPositionSensitivity;
    public float headLerp;
    public Vector3 headStartPosition;
    public Vector3 cameraStartPosition;
    public GameObject canvas;

	// Use this for initialization
	void Start () {
        relativePosition = player.transform.position - transform.position;
        cameraStartPosition = cameraObject.transform.localPosition;
        headStartPosition = InputTracking.GetLocalPosition(VRNode.Head);
        NoVR();
    }
	
	// Update is called once per frame
	void Update () {
        //print(InputTracking.GetLocalPosition(VRNode.Head));
        Move();
        TransformVR();
        if (Input.GetKeyDown("r"))
        {
            print("Recentered");
            InputTracking.Recenter();
        }

	}
    /*
     * Settings specific to oculus only or not oculus only
     * 
     * */
    void NoVR()
    {
        if (player.GetComponent<PlayerControllerThirdPersonVR>().vrEnabled == false)
        {
            speed *= 2;
            canvas.GetComponent<RectTransform>().localPosition = new Vector3(-0.5f, 0.5f, -5);
            canvas.GetComponent<RectTransform>().eulerAngles = new Vector3(10,3,0);
            
        }
        else
        {
            canvas.transform.parent = player.transform;
        }
    }

    void Move()
    {
        Vector3 newPosition = player.transform.position - relativePosition;

        transform.position = Vector3.Lerp(transform.position, newPosition, speed);
    }

    /* Translate camera based on head position*/
    void TransformVR()
    {
        Vector3 headPosition = (InputTracking.GetLocalPosition(VRNode.Head)) * headPositionSensitivity;
        //print(headPosition);
        cameraObject.transform.localPosition = Vector3.Lerp(cameraObject.transform.localPosition, cameraStartPosition + headPosition, headLerp);
    }
}
