using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {
    public float speed;

    public bool spinX;
    public bool spinY;
    public bool spinZ;
    public int x;
    public int y;
    public int z;
    // Use this for initialization
    void Start()
    {


	}

	// Update is called once per frame
	void Update () {
        if (spinX)
        {
            x = 1;
        }
        else
        {
            x = 0;
        }
        if (spinY)
        {
            y = 1;
        }
        else
        {
            y = 0;
        }
        if (spinZ)
        {
            z = 1;
        }
        else
        {
            z = 0;
        }
        SpinAll();
    }

    void SpinAll()
    {
        transform.rotation *= Quaternion.AngleAxis(speed * Time.deltaTime, new Vector3(x, y, z));
    }

    void SpinX()
    {
        //transform.eulerAngles += new Vector3(speed, 0, 0);
        transform.rotation *= Quaternion.AngleAxis(speed * Time.deltaTime, new Vector3(1,0,0));
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
