using UnityEngine;
using System.Collections;

public class SpinControl : HackingConsole
{
    public override void SetCorrectSubLineValue()
    { 
        foreach (GameObject subLine in lines)
        {

            Line lCode = subLine.GetComponent<Line>();
            if (lCode.tag == "spin")
            {
                lCode.SetSubButtonText("false");
            }
            if (lCode.tag == "spinSpeed1")
            {
                Spin1 controller = controllingObject.GetComponent<Spin1>();
                lCode.SetSubButtonText(controller.speed.ToString());
            }
            else if (lCode.tag == "spinSpeed2")
            {
                Spin1 controller2 = controllingObject2.GetComponent<Spin1>();
                lCode.SetSubButtonText(controller2.speed.ToString());
            }
            else if (lCode.tag == "spinSpeed3")
            {
                Spin1 controller3 = controllingObject3.GetComponent<Spin1>();
                lCode.SetSubButtonText(controller3.speed.ToString());
            }
        }
    }
    public override void UpdateRealVariables()
    {
   
        foreach (GameObject subLine in lines)
        {
            Line lCode = subLine.GetComponent<Line>();
            
            if (lCode.tag == "spin")
            {
                Spin1 controller = controllingObject.GetComponent<Spin1>();
                Spin1 controller2 = controllingObject2.GetComponent<Spin1>();
                Spin1 controller3 = controllingObject3.GetComponent<Spin1>();
                controller.spinZ = bool.Parse(lCode.GetSubText());
                controller2.spinZ = bool.Parse(lCode.GetSubText());
                controller3.spinZ = bool.Parse(lCode.GetSubText());
            }
            if (lCode.tag == "spinSpeed1")
            {
                Spin1 controller = controllingObject.GetComponent<Spin1>();
                float newVal = float.Parse(lCode.GetSubText());
                if (newVal < 0)
                {
                    controller.speed = 0;
                    lCode.SetSubButtonText("0");
                }
                else if (newVal > 3)
                {
                    controller.speed = 3;
                    lCode.SetSubButtonText("3");
                }
                else 
                {
                    controller.speed = newVal;
                  
                }
        
            }
            if (lCode.tag == "spinSpeed2")
            {
                float newVal = float.Parse(lCode.GetSubText());
                Spin1 controller2 = controllingObject2.GetComponent<Spin1>();
                if (newVal < 0)
                {
                    controller2.speed = 0;
                    lCode.SetSubButtonText("0");
                }
                else if (newVal > 3)
                {
                    controller2.speed = 3;
                    lCode.SetSubButtonText("3");
                }
                else
                {
                    controller2.speed = newVal;

                }
            }
            if (lCode.tag == "spinSpeed3")
            {
                float newVal = float.Parse(lCode.GetSubText());
                Spin1 controller3 = controllingObject3.GetComponent<Spin1>();
                if (newVal < 0)
                {
                    controller3.speed = 0;
                    lCode.SetSubButtonText("0");
                }
                else if (newVal > 3)
                {
                    controller3.speed = 3;
                    lCode.SetSubButtonText("3");
                }
                else
                {
                    controller3.speed = newVal;

                }
            }
        }
    }
}

