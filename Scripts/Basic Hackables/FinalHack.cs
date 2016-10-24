using UnityEngine;
using System.Collections;

public class FinalHack : HackingConsole
{
    private bool disableElevator;

    public override void SetCorrectSubLineValue()
    {
        foreach (GameObject subLine in lines)
        {
            Line lCode = subLine.GetComponent<Line>();
            if (lCode.tag == "disableEleavtor")
            {
                lCode.SetSubButtonText("true");
            }
            if (lCode.tag == "elevatorHeight")
            {
                Elevator controller = controllingObject.GetComponent<Elevator>();
                lCode.SetSubButtonText(controller.openHeight.ToString());
            }
            if (lCode.tag == "deactivateIntensity1")
            {
                SecurityCamera controller2 = controllingObject2.GetComponent<SecurityCamera>();
                lCode.SetSubButtonText(controller2.deactivateIntensity.ToString());
            }
            if (lCode.tag == "deactivateIntensity2")
            {
                SecurityCamera controller3 = controllingObject3.GetComponent<SecurityCamera>();
                lCode.SetSubButtonText(controller3.deactivateIntensity.ToString());
            }
        }

    }
    public override void UpdateRealVariables()
    {

        foreach (GameObject subLine in lines)
        {
            Line lCode = subLine.GetComponent<Line>();
            if (lCode.tag == "disableEleavtor")
            {
                disableElevator = bool.Parse(lCode.GetSubText());
                if (!disableElevator)
                {
                    Elevator controller = controllingObject.GetComponent<Elevator>();
                    controller.open = true;
                }
            }

            if (lCode.tag == "elevatorHeight")
            {
                Elevator controller = controllingObject.GetComponent<Elevator>();
                float newVal = float.Parse(lCode.GetSubText());
                controller.openHeight = newVal;
            }
            if (lCode.tag == "deactivateIntensity1")
            {
                SecurityCamera controller = controllingObject2.GetComponent<SecurityCamera>();
                float newVal = float.Parse(lCode.GetSubText());
                controller.deactivateIntensity = newVal;
                if (newVal >= 20)
                {
                    controller.permaSpotted = false;
                    controller.playerSeen = false;
                }
            }
            if (lCode.tag == "deactivateIntensity2")
            {
                SecurityCamera controller = controllingObject3.GetComponent<SecurityCamera>();
                float newVal = float.Parse(lCode.GetSubText());
                controller.deactivateIntensity = newVal;
                if (newVal >= 20)
                {
                    controller.permaSpotted = false;
                    controller.playerSeen = false;
                }


            }
        }
    }
}
