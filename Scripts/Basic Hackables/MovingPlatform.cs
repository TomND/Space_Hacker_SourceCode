using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {


    public int speed;
    public GameObject[] points; // points to move too
    public bool enabled;
    public int currentPoint;
    public float minDistance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}


    public void Move()
    {
        if (enabled)
        {
            if(Vector3.Distance(transform.position, points[currentPoint].transform.position) < minDistance)
            {
                if(currentPoint < points.Length - 1)
                {
                    currentPoint += 1;
                }
                else
                {
                    currentPoint = 0;
                }
                
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].transform.position, speed * Time.deltaTime);
            }

        }
    }

    public Vector3 VectorVelocity()
    {
        Vector3 velocity = points[currentPoint].transform.position - transform.position;
        velocity *= speed * Time.deltaTime ;

         return (Vector3.MoveTowards(transform.position, points[currentPoint].transform.position, speed * Time.deltaTime));
        
        //return Vector3.zero;
    }

}
