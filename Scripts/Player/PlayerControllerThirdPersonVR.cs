using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class PlayerControllerThirdPersonVR : MonoBehaviour {
/*
   Vr enabled player controller. handles movement and camera handling.

   @type Animator: animator
        Reference to animator objects
   @type Rigidbody: controller
        Reference to Rigidbody objects
   @type int: speed
        movement speed
   @type GameObject: cameraObject
        Reference to the cameraObject
   @type UIController: uiController
        Reference to the uiController
   @type float: gravity
        Strength of gravity
   @type float: jumpSpeed
        The height that the player jumps
   @type float: rotationSpeed
        The speed that the player rotates
   @type float: rotationDepth
        max X rotation depth allowed
   @type float: deadZone
        Joystick deadZone that does not register movement
   @type CharacterController: charController
        Reference to the CharacterController objects
   @type Vector3: moveDirection
        The direction the player is moving in
   @type Vector3: lastGroundedMove
        last movement vector when last on ground
   @type float: lastGroundTime
        Last time on ground
   @type bool: jumpEnabled
        if true, jumping is allowed
   @type bool: rollEnabled
        if true, rolling is allowed
   @type Vector3: forward
        The players forward Vector3
   @type bool: doubleJump
        True if doublejumping is allowed
   @type float: gravityModifier
        Modifys the strength of gravvity
   @type float: rayDistance
        Distance that ground raycast travels
   @type bool: vrEnabled
        true if vr is supposed to be active
   @type bool: activeTutorial
        true when a tutorial is active




 */
[HideInInspector]
public Animator animator;
private Rigidbody controller;
public int speed;
public GameObject cameraObject;
private UIController uiController;
public float gravity;
public float jumpSpeed;
public float rotationSpeed;
    public float rotationDepth;
    public Vector3 initialRotation;
public float deadZone;                   // movement left stick deadzone.
private CharacterController charController;
[HideInInspector]
public Vector3 moveDirection = Vector3.zero;
public Vector3 lastGroundedMove;
private float lastGroundTime;    // last time on ground.
public bool jumpEnabled;
public bool rollEnabled;
public float minRotateAngle;
public float MaxRotateAngle;
public Vector3 forward;
public bool doubleJump;
public float gravityModifier;
public float rayDistance;
    public bool vrEnabled;
    public bool activeTutorial;
    private Vector3 platformVelocity;
    private bool onPlatform;
// Use this for initialization
void Start()
{
        charController = GetComponent<CharacterController>();
        controller     = GetComponent<Rigidbody>();
        animator       = GetComponentInChildren<Animator>();
        uiController   = GetComponent<UIController>();
        forward = transform.eulerAngles;
        InputTracking.Recenter();
        initialRotation = transform.eulerAngles;
}

// Update is called once per frame
void Update()
{
        AnimationManager();
        if (uiController.ActiveInventory() || activeTutorial)
        {
                moveDirection.x = 0;
                moveDirection.z = 0;
                charController.Move(moveDirection * Time.deltaTime);
        }
        else
        {
                MovementManager();
                RotationManager();
        }
        RayCastGrounded();
}


/*
Returns angle from joystick, with up being zero.
*/
float JoyToRadius(float x,float y)
{
        float angle;
        return Mathf.Atan2(x, y) * Mathf.Rad2Deg;


}

// checks for ground using raycast
// returns true if player is on the ground.
// false if not on ground.
bool RayCastGrounded() {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        if (Physics.Raycast(transform.position, Vector3.down,rayDistance, layerMask))
        {
                return true;
        }
        else
        {
                return false;
        }
}

/*
handles and applys all movement calculations and applys them using the CharacterController
*/
void MovementManager()
{
        float inputX = Input.GetAxis("Left Stick X Axis");
        float inputY = -Input.GetAxis("Left Stick Y Axis");
        JoyToRadius(inputX,inputY);

        if ((inputX < deadZone) && (inputX > -deadZone))
        {
                inputX = 0;
        }
        if ((inputY < deadZone) && (inputY > -deadZone))
        {
                inputY = 0;
        }

        if(inputX != 0 || inputY != 0)
        {
                float angle = JoyToRadius(inputX, inputY);
                float current = transform.eulerAngles.y;

                if(angle < 0)
                {
                        angle = 360 + angle;
                }
                if (current < 90 && angle > 180)
                {
                        transform.eulerAngles = Vector3.Slerp(new Vector3(0, 360, 0) + transform.eulerAngles, new Vector3(0, angle, 0), 0.4f);
                }
                else if(current > 180 && angle < 90)
                {
                        transform.eulerAngles = Vector3.Slerp( transform.eulerAngles, new Vector3(0, 360, 0) + new Vector3(0, angle, 0), 0.4f);
                }
                else
                {
                        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, new Vector3(0, angle, 0), 0.4f);
                }
                //print(angle);

        }


        if ((charController.isGrounded || RayCastGrounded()) && (animator.GetBool("Roll") == false) && !(uiController.ActiveInventory() || activeTutorial))
        {
                lastGroundTime = Time.time;
                animator.SetBool("Jump", false);
                animator.SetBool("DoubleJump", false);
                animator.SetBool("FreeFall", false);
                animator.SetBool("OutOfJumps", false);
                //float yTemp = moveDirection.y;
                moveDirection = ((transform.forward * Mathf.Max(Mathf.Abs(inputX), Mathf.Abs(inputY))) * speed) + new Vector3(0,moveDirection.y,0);
                //moveDirection *= speed;

                if (Input.GetButtonDown("A_Button") && jumpEnabled)
                {
                        moveDirection.y = jumpSpeed;
                        animator.SetBool("Jump", true);
                }
                if (Mathf.Max(Mathf.Abs(inputX), Mathf.Abs(inputY)) > 0.5f)
                {

                        if (Input.GetButtonDown("B_Button") && rollEnabled)
                        {
                                animator.SetBool("Roll", true);
                        }
                }
        }
        else
        {
                moveDirection = (transform.forward * Mathf.Max(Mathf.Abs(inputX), Mathf.Abs(inputY))* speed) + new Vector3(0,moveDirection.y,0);
                if (animator.GetBool("Jump") == false && Time.time > lastGroundTime + 0.2f)
                {
                        animator.SetBool("FreeFall", true);
                }
                if (animator.GetBool("Roll") == true)
                {
                        moveDirection = transform.forward * speed;
                }
                if(!doubleJump && Input.GetButtonDown("A_Button") && jumpEnabled && animator.GetBool("DoubleJump") ==  false)
                {
                        animator.SetBool("DoubleJump", true);
                        moveDirection.y = jumpSpeed*1.25f;
                        gravityModifier = 0.5f;
                }
                else
                {
                        gravityModifier = 1f;
                }
        }



        moveDirection.y -= gravity * gravityModifier * Time.deltaTime;
        moveDirection += platformVelocity;
        charController.Move(moveDirection * Time.deltaTime);
}
/*
animation event called when doublejump animation ends
*/
public void DoubleJumpEnded()
{
        animator.SetBool("DoubleJump", false);
}

/*
animation event called when doublejump animation starts
*/
public void DoubleJumpStart()
{
        animator.SetBool("OutOfJumps", true);
}

// called when a collider enters the trigger on the player.
void OnTriggerEnter(Collider other)
{
        if(other.tag == "MovingPlatform")
        {
            onPlatform = true;
            //platformVelocity = transform.position - other.gameObject.GetComponent<MovingPlatform>().VectorVelocity();
            transform.parent = other.transform;
        }
}

//caled when a collider exits the trigger on the player
void OnTriggerExit(Collider other)
{
        if(other.tag == "MovingPlatform")
        {
            onPlatform = false;
            //platformVelocity = new Vector3(platformVelocity.x, 0, platformVelocity.z);
            transform.parent = null;
        }

}

//animation event called when roll animation ends
public void RollFinished()
{
        animator.SetBool("Roll", false);
}

//animation event called when hit animatino ends
public void HitEnd()
{
        animator.SetBool("Hit", false);
}

/*
 *	manages player rotation using right stick only when VR is disabled
 */
void RotationManager()
{
        if(vrEnabled)
        {
            return;
        }
        float xRotation = Input.GetAxis("Right Stick X Axis") * rotationSpeed;

        cameraObject.transform.eulerAngles += new Vector3(0, xRotation, 0);

        float yRotation = Input.GetAxis("Right Stick Y Axis") * rotationSpeed;

        Vector3 eulerCamera = cameraObject.transform.eulerAngles;

        cameraObject.transform.eulerAngles = new Vector3(eulerCamera.x, Mathf.Lerp(eulerCamera.y, initialRotation.y + xRotation * rotationDepth, rotationSpeed), eulerCamera.z);



        /*
        if ((cameraObject.transform.eulerAngles.x > MaxRotateAngle && cameraObject.transform.eulerAngles.x < 120) && yRotation > 0)
        {

        }
        else if ((cameraObject.transform.eulerAngles.x > 200 && cameraObject.transform.eulerAngles.x < minRotateAngle ) && yRotation < 0)
        {

        }
        else
        {
                cameraObject.transform.eulerAngles += new Vector3(Input.GetAxis("Right Stick Y Axis") * rotationSpeed, 0, 0);
        }*/

    }

    /*
     * Manages animations, not putting lots of effort to make this work or look good yet, so don't worry to much about it
     */
    void AnimationManager()
{
        float lerpX = Mathf.Lerp(animator.GetFloat("MovementX"), Input.GetAxis("Left Stick X Axis"), Time.deltaTime * 5f); // animator.GetFloat("MovementX");
        float lerpY = Mathf.Lerp(animator.GetFloat("MovementY"), -Input.GetAxis("Left Stick Y Axis"), Time.deltaTime * 5f);

        animator.SetFloat("MovementX", lerpX);
        animator.SetFloat("MovementY", lerpY);
        //animator.SetFloat("MovementX", Input.GetAxis("Left Stick X Axis"));
        //animator.SetFloat("MovementY",-Input.GetAxis("Left Stick Y Axis"));
}
}
