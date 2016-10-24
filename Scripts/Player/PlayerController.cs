using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
   /*
    * This class manages everything related to player movement and controls.
    */
   private Animator     animator;
   private Rigidbody    controller;
   public int           speed;
   public GameObject    cameraObject;
   private UIController uiController;

   // Use this for initialization
   void Start()
   {
      controller   = GetComponent <Rigidbody>();
      animator     = GetComponentInChildren<Animator>();
      uiController = GetComponent <UIController>();
   }

   // Update is called once per frame
   void Update()
   {
      AnimationManager();
        if (uiController.ActiveInventory())
        {
            controller.MovePosition(transform.position);
         }
      else{
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
      float   xMovement = Input.GetAxis("Left Stick X Axis") * speed * Time.deltaTime;
      float   zMovement = Input.GetAxis("Left Stick Y Axis") * speed * Time.deltaTime;
        Vector3 velocity = transform.forward * -zMovement + transform.right * xMovement;

        controller.MovePosition(transform.position + velocity);
      //controller.velocity = velocity;
   }

   /*	This manages the players rotation. works similar to MovementManager but changes rotation values
    *	Eulerangles returns values in degrees. we edit both the plater rotation and the camera objects rotation
    */
   void RotationManager()
   {
      transform.eulerAngles += new Vector3(0, Input.GetAxis("Right Stick X Axis"), 0);
      cameraObject.transform.eulerAngles += new Vector3(Input.GetAxis("Right Stick Y Axis"), 0, 0);
   }

   /*
    * Manages animations, not putting lots of effort to make this work or look good yet, so don't worry to much about it
    */
   void AnimationManager()
   {
      animator.SetFloat("movementX", Input.GetAxis("Left Stick X Axis"));
      animator.SetFloat("movementY", Input.GetAxis("Left Stick Y Axis"));
   }
}
