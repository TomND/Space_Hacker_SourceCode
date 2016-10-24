using UnityEngine;
using System.Collections;

public class Revolve : MonoBehaviour
{

    public GameObject pivot;
    public bool revolving;
    public void Start()
    {

    }
    public void Update()
    {
        if (revolving)
        {
            transform.RotateAround(pivot.transform.position, new Vector3(0, 1, 0), 100 * Time.deltaTime);
        }
    }
   
}
