using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour
{
    /*
     * Manages door open and close state
     *
     */
    private Vector3 startPosition;
    private Vector3 openPosition;
    public float openHeight = 0;
    public float openSpeed;
    public bool open = true;
    public bool bigDoor;
    public GameObject leftDoor;
    public GameObject rightDoor;
    private Vector3 openLeft;
    private Vector3 openRight;
    private Vector3 closedLeft;
    private Vector3 closedRight;
    public bool updateDoorState;
    public float openHeightTemp;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;

        openPosition = startPosition + new Vector3(0, openHeight, 0);
        openHeightTemp = openHeight + 1 - 1;
        if (bigDoor)
        {
            closedLeft = leftDoor.transform.localPosition;
            closedRight = rightDoor.transform.localPosition;
            openLeft = closedLeft - new Vector3(openHeight, 0, 0);
            openRight = closedRight + new Vector3(openHeight, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        openPosition = startPosition + new Vector3(0, openHeight, 0);
        if (updateDoorState)
        {

            DoorController();

        }
    }

    /*
     * Manages the open/close state of a door. Lerps towards the open position or the closed position
     */
    public void DoorController()
    {
        if (open)
        {
            transform.position = Vector3.Lerp(transform.position, openPosition, openSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, openSpeed * Time.deltaTime);
        }
    }


}



