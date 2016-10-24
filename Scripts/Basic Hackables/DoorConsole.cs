using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DoorConsole : HackingConsole
{
   public override void UpdateRealVariables()
   {
        if (hackingUI.activeSelf == false)
        {
            return;
        }
        foreach (GameObject subLine in lines){
          Line lCode = subLine.GetComponent <Line>();
          if(lCode.tag == "height"){
                Door door = controllingObject.GetComponent<Door>();
                float newVal = float.Parse(lCode.GetSubText());
                if (newVal < 0)
                {
                    door.openHeight = 0;
                    lCode.SetSubButtonText("0");
                }
                else if (newVal > 30)
                {
                    door.openHeight = 30;
                    lCode.SetSubButtonText("30");
                }
                else
                {
                    door.openHeight = newVal;
                }


             }
            else if (lCode.tag == "height2")
            {
                Door door = controllingObject2.GetComponent<Door>();
                float newVal = float.Parse(lCode.GetSubText());
                if (newVal < 0)
                {
                    door.openHeight = 0;
                    lCode.SetSubButtonText("0");
                }
                else if (newVal > 30)
                {
                    door.openHeight = 30;
                    lCode.SetSubButtonText("30");
                }
                else
                {
                    door.openHeight = newVal;
                }


            }
            else if (lCode.tag == "height3")
            {
                Door door = controllingObject3.GetComponent<Door>();
                float newVal = float.Parse(lCode.GetSubText());
                if (newVal < 0)
                {
                    door.openHeight = 0;
                    lCode.SetSubButtonText("0");
                }
                else if (newVal > 30)
                {
                    door.openHeight = 30;
                    lCode.SetSubButtonText("30");
                }
                else
                {
                    door.openHeight = newVal;
                }


            }
            else if(lCode.tag == "access"){
                Door door = controllingObject.GetComponent<Door>();
                door.open = bool.Parse(lCode.GetSubText());
            }
            else if (lCode.tag == "tripleAccess")
            {
                Door door = controllingObject.GetComponent<Door>();
                door.open = bool.Parse(lCode.GetSubText());
                door = controllingObject2.GetComponent<Door>();
                door.open = bool.Parse(lCode.GetSubText());
                door = controllingObject3.GetComponent<Door>();
                door.open = bool.Parse(lCode.GetSubText());
            }
            else if(lCode.tag == "enable")
            {
                Door door = controllingObject.GetComponent<Door>();
                door.open = bool.Parse(lCode.GetSubText());
            }
          }
   }

   public override void SetCorrectSubLineValue()
   {
        if (hackingUI.activeSelf == false)
        {
            return;
        }
      foreach(GameObject subLine in lines){
          Line lCode = subLine.GetComponent <Line>();
          if(lCode.tag == "height"){
             Door door = controllingObject.GetComponent <Door>();
             lCode.SetSubButtonText(door.openHeight.ToString());
             }
            else if (lCode.tag == "height2")
            {
                Door door = controllingObject2.GetComponent<Door>();
                lCode.SetSubButtonText(door.openHeight.ToString());
            }
            else if (lCode.tag == "height3")
            {
                Door door = controllingObject3.GetComponent<Door>();
                lCode.SetSubButtonText(door.openHeight.ToString());
            }
            else if(lCode.tag == "access"){
                  lCode.SetSubButtonText("false");
                  }
            else if (lCode.tag == "tripleAccess")
            {
                Door door = controllingObject.GetComponent<Door>();
                lCode.SetSubButtonText(door.open.ToString());
                door = controllingObject.GetComponent<Door>();
                lCode.SetSubButtonText(door.open.ToString());
                door = controllingObject.GetComponent<Door>();
                lCode.SetSubButtonText(door.open.ToString());
            }
            else if(lCode.tag == "override"){
                  lCode.SetSubButtonText("false");
                  }
            else if (lCode.tag == "enable")
            {
                Door door = controllingObject.GetComponent<Door>();
                lCode.SetSubButtonText(door.open.ToString());
            }
            else if (lCode.tag == "decryptKey")
            {
                lCode.SetSubButtonText("0");
            }
        }
   }

   public bool ValidReq(int funcIndex)
   {
     return true;/*
      GameObject functionObject = hackingUI.GetComponent <LineManager>().functions[funcIndex];
      Function   function       = functionObject.GetComponent <Function>();
      string     req            = function.ReturnReq();
      string     reqVal         = function.ReturnReqVal();

      foreach(GameObject subLine in lines){
          Line lCode = subLine.GetComponent <Line>();
          if(lCode.tag == req){
             if(lCode.subBoxText.text == reqVal){
                return(true);
                }
             else{
                 return(false);
                 }
             }
          }
      return(false);*/
   }

   public override void ManageFunctions()
   {
      if(isFunctionCalled[0] && ValidReqs(0)){
         OpenDoor();
         }
      else{
          CloseDoor();
          }
   }

   public void OpenDoor()
   {
      Door door = controllingObject.GetComponent <Door>();

      door.open = true;
   }

   public void CloseDoor()
   {
      Door door = controllingObject.GetComponent <Door>();

      door.open = false;
   }
}
