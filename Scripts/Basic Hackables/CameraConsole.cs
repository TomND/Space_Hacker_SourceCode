using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;



public class CameraConsole : HackingConsole
{
   public override void UpdateRealVariables()
   {
      foreach (GameObject subLine in lines)
      {
         Line lCode = subLine.GetComponent<Line>();
         if (lCode.tag == "speed")
         {
            Security_Camera cam = controllingObject.GetComponent<Security_Camera>();
            cam.hackableSpeed = int.Parse(lCode.GetSubText());
         }
         else if (lCode.tag == "spotted")
         {
            Security_Camera cam = controllingObject.GetComponent<Security_Camera>();
            cam.playerSeen = bool.Parse(lCode.GetSubText());
         }
      }
   }

   public override void SetCorrectSubLineValue()
   {
      foreach (GameObject subLine in lines)
      {
         Line lCode = subLine.GetComponent<Line>();
         if (lCode.tag == "speed")
         {
            Security_Camera cam = controllingObject.GetComponent<Security_Camera>();
            lCode.SetSubButtonText(cam.hackableSpeed.ToString());
         }
         else if (lCode.tag == "spotted")
         {
            Security_Camera cam = controllingObject.GetComponent<Security_Camera>();
            lCode.SetSubButtonText(cam.playerSeen.ToString());
         }
      }
   }

   /*
    * Returns true if the line with tag Req has the value: reqVal
    * False otherwise
    */

   /*   public override bool CheckForValidReq(int funcIndex)
    * {
    *     foreach(GameObject subLine in lines){
    *       Line lCode = subLine.GetComponent <Line>();
    *       if(lCode.tag == req){
    *          if(lCode.subBoxText.text == reqVal){
    *             return(true);
    *             }
    *          else{
    *              return(false);
    *              }
    *          }
    *       }
    *   return(false);
    * }*/

   /*
    * Returns true if the line with tag Req has the value: reqVal
    * False otherwise
    */
   public bool ValidReq(int funcIndex)
   {
     return true;/*
      GameObject functionObject = hackingUI.GetComponent<LineManager>().functions[funcIndex];
      Function   function       = functionObject.GetComponent<Function>();
      string     req            = function.ReturnReq();
      string     reqVal         = function.ReturnReqVal();

      foreach (GameObject subLine in lines)
      {
         Line lCode = subLine.GetComponent<Line>();
         if (lCode.tag == req)
         {
            if (lCode.subBoxText.text == reqVal)
            {
               return(true);
            }
            else
            {
               return(false);
            }
         }
      }
      return false;*/
   }

   public override void ManageFunctions()
   {
      if (isFunctionCalled[0] && ValidReq(0))
      {
         PlayerDetected();
      }
   }

   public void PlayerDetected()
   {
      Security_Camera cam = controllingObject.GetComponent<Security_Camera>();

      cam.PlayerSpotted();
   }
}
