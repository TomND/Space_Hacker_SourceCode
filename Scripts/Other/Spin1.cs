using UnityEngine;
using System.Collections;

public class Spin1 : MonoBehaviour {
    public float speed;

    public bool spinX;
    public bool spinY;
    public bool spinZ;
    // Use this for initialization
    void Start()
    {


	}

	// Update is called once per frame
	void Update () {
        if (spinX)
        {
            SpinX();
        }
        if (spinY)
        {
            SpinY();
        }
        if (spinZ)
        {
            SpinZ();
        }
    }

    void SpinX()
    {
        transform.eulerAngles += new Vector3(speed, 0, 0);
    }

    void SpinY()
    {
        transform.eulerAngles += new Vector3(0, speed, 0);
    }

    void SpinZ()
    {
        transform.eulerAngles += new Vector3(0, 0, speed);
    }


}
