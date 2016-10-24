using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class PlatformConsole : HackingConsole
{
   public override void UpdateRealVariables()
   {
      foreach(GameObject subLine in lines){
          Line lCode = subLine.GetComponent <Line>();
          if(lCode.tag == "speed"){
             MovingPlatform platform = controllingObject.GetComponent <MovingPlatform>();
                float newVal = int.Parse(lCode.GetSubText());
                if (newVal < 0)
                {
                    platform.speed = 0;
                    lCode.SetSubButtonText("0");
                }
                else if(newVal > 20)
                {
                    platform.speed = 20;
                    lCode.SetSubButtonText("20");
                }
                else
                {
                    platform.speed = int.Parse(lCode.GetSubText());
                }

             }
          else if(lCode.tag == "access"){
                  }
          }
   }

   public override void SetCorrectSubLineValue()
   {
      foreach(GameObject subLine in lines){
          Line lCode = subLine.GetComponent <Line>();
          if(lCode.tag == "speed"){
                MovingPlatform platform = controllingObject.GetComponent<MovingPlatform>();
                lCode.SetSubButtonText(platform.speed.ToString());
             }
          else if(lCode.tag == "access"){
                  lCode.SetSubButtonText("false");
                  }
          else if(lCode.tag == "override"){
                  lCode.SetSubButtonText("false");
                  }
          }
   }

   public bool ValidReq(int funcIndex)
   {throw new Exception("This method has been removed: Used ValidReqs instead");
      return(false);
   }

   public override void ManageFunctions()
   {
      if(isFunctionCalled[0] && ValidReqs(0)){
            EnablePlatform();
         }
      else{
            DisablePlatform();
          }
   }

   public void EnablePlatform()
   {
        MovingPlatform platform = controllingObject.GetComponent<MovingPlatform>();

        platform.enabled = true;
        controllingObject2.GetComponent<NextLevel>().enabled = true;
        controllingObject2.transform.localScale = Vector3.Lerp(controllingObject2.transform.localScale, new Vector3(30,30,30),0.01f);
   }

   public void DisablePlatform()
   {
        MovingPlatform platform = controllingObject.GetComponent<MovingPlatform>();

        platform.enabled = false;
   }
}
