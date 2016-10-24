using UnityEngine;
using System.Collections;

public class MultiObjectController : MonoBehaviour {
   public GameObject[] objects;

   public bool  spin;
   public bool  move;
   private bool moveEnabled = false;
   private bool spinEnabled = false;

   // Use this for initialization
   void Start()
   {
   }

   // Update is called once per frame
   void Update()
   {
      ManageSpin();
      ManageMovement();
   }

   void ManageSpin()
   {
      if (spin)
      {
         foreach (GameObject obj in objects)
         {
            obj.GetComponent<Spin>().enabled = true;
         }
         spinEnabled = true;
      }
      else if (spinEnabled)
      {
         foreach (GameObject obj in objects)
         {
            obj.GetComponent<Spin>().enabled = false;
         }
         spinEnabled = false;
      }
   }

   void ManageMovement()
   {
      if (move)
      {
         foreach (GameObject obj in objects)
         {
            obj.GetComponent<MovingPlatform>().enabled = true;
         }
         moveEnabled = true;
      }
      else if (moveEnabled)
      {
         foreach (GameObject obj in objects)
         {
            obj.GetComponent<MovingPlatform>().enabled = false;
            moveEnabled = false;
         }
      }
   }
}
