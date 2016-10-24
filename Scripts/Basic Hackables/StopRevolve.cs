using UnityEngine;
using System.Collections;

public class StopRevolve : HackingConsole
{
    public override void SetCorrectSubLineValue()
    {
        foreach (GameObject subLine in lines)
        {

            Line lCode = subLine.GetComponent<Line>();
            if (lCode.tag == "revolve")
            {
                lCode.SetSubButtonText("true");
            }
        }
    }
    public override void UpdateRealVariables()
    {

        foreach (GameObject subLine in lines)
        {
            Line lCode = subLine.GetComponent<Line>();

            if (lCode.tag == "revolve")
            {
                Revolve controller = controllingObject.GetComponent<Revolve>();
                controller.revolving = bool.Parse(lCode.GetSubText());
            }
        }
    }
}
