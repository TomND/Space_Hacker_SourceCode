using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {
   public GameObject[] tutorials;
   public int          index = 0;
   public bool         startTutorial;
   public bool         disableAtEnd;
   public GameObject   triggerObject;
   public PlayerControllerThirdPersonVR player;
   // Use this for initialization
   void Start()
   {
      if (triggerObject != null)
      {
         triggerObject.GetComponent<Triggered>().tutorial = this;
      }
      if (startTutorial)
      {
         tutorials[index].SetActive(true);
      }
   }

   // Update is called once per frame
   void Update()
   {
      transform.SetAsLastSibling();
      if (startTutorial)
      {
         if (ManageInputType(tutorials[index]))
         {
            tutorials[index].SetActive(false);
            if (index < tutorials.Length - 1)
            {
               index += 1;
               tutorials[index].SetActive(true);
            }
            else
            {
               tutorials[index].SetActive(false);
               if (player != null)
               {
                  player.activeTutorial = false;
                  player = null;
               }

               if (disableAtEnd)
               {
                  gameObject.SetActive(false);
               }
            }
         }
      }
   }

   bool ManageInputType(GameObject tut)
   {
      if (tut.tag == "Untagged")
      {
         return(false);
      }
      if (tut.tag == "Left Stick")
      {
         if ((Mathf.Abs(Input.GetAxis("Left Stick X Axis")) >= 0.8f) || (Mathf.Abs(Input.GetAxis("Left Stick Y Axis")) >= 0.8f))
         {
            return(true);
         }
      }
      else if (tut.tag == "Right Stick")
      {
         if ((Mathf.Abs(Input.GetAxis("Right Stick X Axis")) >= 0.8f) || (Mathf.Abs(Input.GetAxis("Right Stick Y Axis")) >= 0.8f))
         {
            return(true);
         }
      }
      else
      {
         if (Input.GetButtonUp(tut.tag))
         {
            return(true);
         }
      }
      return(false);
   }

   //delete this ?
   void OnTriggerEnter(Collider other)
   {
      if ((other.tag == "Player") && !startTutorial)
      {
         print("here");
         player = other.gameObject.GetComponent<PlayerControllerThirdPersonVR>();
         player.activeTutorial = true;
         other.gameObject.GetComponent<PlayerControllerThirdPersonVR>().activeTutorial = true;
         startTutorial = true;
         tutorials[index].SetActive(true);
      }
   }
}
