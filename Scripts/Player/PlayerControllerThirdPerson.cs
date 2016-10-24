using UnityEngine;
using System.Collections;

public class PlayerControllerThirdPerson : MonoBehaviour {
   /*
    * This class manages everything related to player movement and controls.
    */
   [HideInInspector]
   public Animator             animator;
   private Rigidbody           controller;
   public int                  speed;
   public GameObject           cameraObject;
   private UIController        uiController;
   public float                gravity;
   public float                jumpSpeed;
   public float                rotationSpeed;
   public float                deadZone; // movement left stick deadzone.
   private CharacterController charController;
   [HideInInspector]
   public Vector3 moveDirection = Vector3.zero;
    private float lastGroundTime;// last time on ground.
    public bool jumpEnabled;
    public bool rollEnabled;
    public float minRotateAngle;
    public float MaxRotateAngle;
   // Use this for initialization
   void Start()
   {
      charController = GetComponent<CharacterController>();
      controller     = GetComponent<Rigidbody>();
      animator       = GetComponentInChildren<Animator>();
      uiController   = GetComponent<UIController>();
   }

   // Update is called once per frame
   void Update()
   {
      AnimationManager();
      if (uiController.ActiveInventory())
      {
         controller.MovePosition(transform.position);
      }
      else
      {
         MovementManager();
         RotationManager();
      }
   }

   void InventoryControlsManager()
   {
   }

   /*
    *	 This Manages the players movement
    *	 It takes the input, multiplies it by speed, and then by deltaTime so that the speed isin't tied to the fps
    *		we then take the forward and right directions of the player and multiply them by the values created and set this new
    *    value as the player velocity
    */
   void MovementManager()
   {
      /*
       * float   xMovement = Input.GetAxis("Left Stick X Axis") * speed * Time.deltaTime;
       * float   zMovement = Input.GetAxis("Left Stick Y Axis") * speed * Time.deltaTime;
       * Vector3 velocity = transform.forward * -zMovement + transform.right * xMovement;
       * controller.MovePosition(transform.position + velocity);
       */
      float inputX = Input.GetAxis("Left Stick X Axis");
      float inputY = -Input.GetAxis("Left Stick Y Axis");

      if ((inputX < deadZone) && (inputX > -deadZone))
      {
         inputX = 0;
      }
      if ((inputY < deadZone) && (inputY > -deadZone))
      {
         inputY = 0;
      }
      if (charController.isGrounded && (animator.GetBool("Roll") == false))
      {
         lastGroundTime = Time.time;
         animator.SetBool("Jump", false);
         animator.SetBool("FreeFall", false);
         moveDirection  = new Vector3(inputX, 0, inputY);
         moveDirection  = transform.TransformDirection(moveDirection);
         moveDirection *= speed;
         if ((inputX < 0.5) && (inputX > -0.5) && (inputY > 0))
         {
            if (Input.GetButton("A_Button") && jumpEnabled)
            {
               moveDirection.y = jumpSpeed;
               animator.SetBool("Jump", true);
            }
            if (Input.GetButtonDown("B_Button") && rollEnabled)
            {
               animator.SetBool("Roll", true);
            }
         }
      }
      else
      {
         if (animator.GetBool("Jump") == false && Time.time > lastGroundTime + 0.5f)
         {
            animator.SetBool("FreeFall", true);
         }
         if (animator.GetBool("Roll") == true)
         {
            moveDirection = transform.forward * speed;
         }
      }



      moveDirection.y -= gravity * Time.deltaTime;
      charController.Move(moveDirection * Time.deltaTime);
      //controller.velocity = velocity;
   }


   void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Platform")
        {
            //transform.parent = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Platform")
        {
            transform.parent = null;
        }

    }

   public void RollFinished()
   {
      animator.SetBool("Roll", false);
   }

   public void HitEnd()
   {
      animator.SetBool("Hit", false);
   }

   /*	This manages the players rotation. works similar to MovementManager but changes rotation values
    *	Eulerangles returns values in degrees. we edit both the plater rotation and the camera objects rotation
    */
   void RotationManager()
   {
        float xRotation = Input.GetAxis("Right Stick X Axis") * rotationSpeed;

        transform.eulerAngles += new Vector3(0, xRotation, 0);

        float yRotation = Input.GetAxis("Right Stick Y Axis") * rotationSpeed;

        if ((cameraObject.transform.eulerAngles.x > MaxRotateAngle && cameraObject.transform.eulerAngles.x < 120) && yRotation > 0)
        {
            
        }
        else if ((cameraObject.transform.eulerAngles.x > 200 && cameraObject.transform.eulerAngles.x < minRotateAngle ) && yRotation < 0)
        {
            
        }
        else
        {
            cameraObject.transform.eulerAngles += new Vector3(Input.GetAxis("Right Stick Y Axis") * rotationSpeed, 0, 0);
        }
        
   }

   /*
    * Manages animations, not putting lots of effort to make this work or look good yet, so don't worry to much about it
    */
   void AnimationManager()
   {
      float lerpX = Mathf.Lerp(animator.GetFloat("MovementX"), Input.GetAxis("Left Stick X Axis"), Time.deltaTime * 5f);  // animator.GetFloat("MovementX");
      float lerpY = Mathf.Lerp(animator.GetFloat("MovementY"), -Input.GetAxis("Left Stick Y Axis"), Time.deltaTime * 5f);

      animator.SetFloat("MovementX", lerpX);
      animator.SetFloat("MovementY", lerpY);
      //animator.SetFloat("MovementX", Input.GetAxis("Left Stick X Axis"));
      //animator.SetFloat("MovementY",-Input.GetAxis("Left Stick Y Axis"));
   }
}
