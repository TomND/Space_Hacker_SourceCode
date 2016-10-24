using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {


    public GameObject camera;
    public GameObject mainCam;
    public GameObject rotatingCamera;
    public GameObject PlayerParent;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
        camera.transform.eulerAngles = new Vector3(rotatingCamera.transform.eulerAngles.x, -rotatingCamera.transform.eulerAngles.y, 0);
	}


    void OnTriggerEnter(Collider other)
    {

        PlayerParent.transform.position = camera.transform.position;
    }
}
