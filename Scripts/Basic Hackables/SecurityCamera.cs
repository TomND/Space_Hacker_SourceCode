using UnityEngine;
using System.Collections;

public class SecurityCamera : MonoBehaviour
{
    public int swivelAngle;
    public float hackableSpeed;//speed value when hacking
    public float swivelSpeed;
    private float startAngle;
    private Light light;
    private bool forward = true;
    public Material redBeam;
    public Material originalBeam;
    public bool permaSpotted;
    public bool playerSeen;
    public bool controlBigDoor;
    public GameObject bigDoor;
    public Transform spawnPoint;
    public float deactivateIntensity;


    // Use this for initialization
    void Start()
    {
        startAngle = transform.eulerAngles.y;
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        swivelSpeed = hackableSpeed / 10f;
        Swivel();
        if (!playerSeen)
        {
            SetCameraDefault();
        }
        else
        {
            PlayerSpotted();
        }
        if (controlBigDoor)
        {

        }
        if (deactivateIntensity >= 20)
        {
            SphereCollider[] myColliders = this.GetComponents<SphereCollider>();
            foreach (SphereCollider c in myColliders)
            {
                c.enabled = false;
            }
        }
    }



    /*
     * Swivels the camera left and right
     */
    void Swivel()
    {
        if (transform.eulerAngles.y > startAngle + swivelAngle)
        {
            forward = false;
        }
        else if (transform.eulerAngles.y < startAngle - swivelAngle)
        {
            forward = true;
        }

        if (forward)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Lerp(transform.eulerAngles.y, 400, swivelSpeed * Time.deltaTime), transform.eulerAngles.z);
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Lerp(transform.eulerAngles.y, 0, swivelSpeed * Time.deltaTime), transform.eulerAngles.z);
        }
    }

    /*
     *	This is called when the trigger collider sees something
     * Calls PlayerSpotted
     */
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerSpotted();
            other.transform.position = spawnPoint.position;
        }
    }

    /*
     * Called when a collider leaves the trigger collider. calls SetCameraDefault
     *
     */
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!permaSpotted)
            {
                playerSeen = false;
                SetCameraDefault();
            }
        }
    }

    /* Makes the camera light red to represent player spotted
     *
     */
    public void PlayerSpotted()
    {
        playerSeen = true;
        light.intensity = 10;
        light.color = Color.red;

        foreach (Transform obj in gameObject.GetComponentsInChildren<Transform>())
        {
            if (obj.tag == "LightBeam")
            {
                MeshRenderer rend = obj.gameObject.GetComponent<MeshRenderer>();
                rend.material = redBeam;
            }
        }
    }

    /*
     * Resets color and intensity to default values
     */
    void SetCameraDefault()
    {
        light.intensity = 3.5f;
        light.color = Color.black;
        foreach (Transform obj in gameObject.GetComponentsInChildren<Transform>())
        {
            if (obj.tag == "LightBeam")
            {
                MeshRenderer rend = obj.gameObject.GetComponent<MeshRenderer>();

                rend.material = originalBeam;
                return;
            }
        }
    }
}
