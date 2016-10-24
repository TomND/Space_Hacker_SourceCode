using UnityEngine;
using System.Collections;
using System;

public class OpenPortal : HackingConsole
{
    public override void UpdateRealVariables()
    {
        foreach (GameObject subLine in lines)
        {
            Line lCode = subLine.GetComponent<Line>();
            if (lCode.tag == "move")
            {
                MultiObjectController controller = controllingObject.GetComponent<MultiObjectController>();
                controller.move = bool.Parse(lCode.GetSubText());

            }
            else if (lCode.tag == "spin")
            {
                MultiObjectController controller = controllingObject.GetComponent<MultiObjectController>();
                controller.spin = bool.Parse(lCode.GetSubText());
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
                MovingPlatform platform = controllingObject.GetComponent<MovingPlatform>();
                lCode.SetSubButtonText(platform.speed.ToString());
            }
            else if (lCode.tag == "move")
            {
                MultiObjectController controller = controllingObject.GetComponent<MultiObjectController>();
                lCode.SetSubButtonText(controller.move.ToString());
            }
            else if (lCode.tag == "spin")
            {
                MultiObjectController controller = controllingObject.GetComponent<MultiObjectController>();
                lCode.SetSubButtonText(controller.spin.ToString());
            }
            else if(lCode.tag == "accessCode"){
              lCode.SetSubButtonText("0");
            }
            else if(lCode.tag == "overrideDefaults"){
              lCode.SetSubButtonText("false");
            }
            else if(lCode.tag == "decryptKey"){
              lCode.SetSubButtonText("0");
            }
        }
    }

    public bool ValidReq(int funcIndex)
    {   throw new Exception("This method has been removed: Used ValidReqs instead");
        return (false);
    }

    public override void ManageFunctions()
    {
        if (isFunctionCalled[0] && ValidReqs(0))
        {
            DisableSpinZ();
        }
        else
        {
            EnableSpinZ();
        }
    }

    public void DisableSpinZ()
    {
        controllingObject.transform.localScale = Vector3.Lerp(controllingObject.transform.localScale, new Vector3(20,20,20),0.001f);
    }

    public void EnableSpinZ()
    {

    }

}
