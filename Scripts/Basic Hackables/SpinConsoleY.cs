using UnityEngine;
using System.Collections;
using System;

public class SpinConsoleY : HackingConsole
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
            DisableSpinY();
        }
        else
        {
            EnableSpinY();
        }
    }

    public void DisableSpinY()
    {
        Spin spin = controllingObject.GetComponent<Spin>();
        spin.spinY = false;
        controllingObject2.transform.localScale = Vector3.Lerp(controllingObject2.transform.localScale, new Vector3(10,10,10),0.01f);
        controllingObject2.GetComponent<Teleport>().enabled = true;
        GetComponent<AudioSource>().enabled = true;
    }

    public void EnableSpinY()
    {
        Spin spin = controllingObject.GetComponent<Spin>();
        spin.spinY = true;
    }

}
