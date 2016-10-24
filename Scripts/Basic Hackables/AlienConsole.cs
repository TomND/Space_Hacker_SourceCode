using UnityEngine;
using System.Collections;

public class AlienConsole : HackingConsole {

    private bool inactive;


    public override void SetCorrectSubLineValue()
    {
        foreach (GameObject subLine in lines)
        {

            Line lCode = subLine.GetComponent<Line>();
            if (lCode.tag == "inactive")
            {
                lCode.SetSubButtonText("false");
            }
            if (lCode.tag == "deactivateIntensity")
            {
                SecurityCamera controller = controllingObject.GetComponent<SecurityCamera>();
                lCode.SetSubButtonText(controller.deactivateIntensity.ToString());
            }
           
        }
    }
    public override void UpdateRealVariables()
    {

        foreach (GameObject subLine in lines)
        {
            Line lCode = subLine.GetComponent<Line>();
            if (lCode.tag == "inactive")
            {
                inactive = bool.Parse(lCode.GetSubText());
            }

                if (lCode.tag == "deactivateIntensity")
            {
                SecurityCamera controller = controllingObject.GetComponent<SecurityCamera>();
                float newVal = float.Parse(lCode.GetSubText());
                controller.deactivateIntensity = newVal;
                if (newVal >=  10 && inactive)
                {
                    controller.permaSpotted = false;
                    controller.playerSeen = false;

                }
                
            }
        }
    }

}
