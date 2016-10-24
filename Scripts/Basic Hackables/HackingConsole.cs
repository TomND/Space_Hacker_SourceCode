using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class HackingConsole : MonoBehaviour
{
   /* This class manages what a Hacking console can access
    *
    *
    */

   public GameObject       hackingUIPrefab;
   public GameObject       hackingUI;
   public GameObject       controllingObject;
   public GameObject       controllingObject2;
   public GameObject       controllingObject3;
   public List<GameObject> lines;
   //public List <GameObject> functions;
   public List<string> functionNames;
   public List<bool>   isFunctionCalled;
   public GameObject   tutorial;
   [HideInInspector]
   public bool tutCalled = false;
   public bool hasTutorial;

   // Use this for initialization
   void Start()
   {
      //isFunctionCalled = new List<bool>(12);//functionNames.Count);
      InitializeIsFunctionCalled();
      //SetCorrectSubLineValue(lines[2]);
   }

   // Update is called once per frame
   void Update()
   {
      if (lines.Count != 0)
      {
         //SetCorrectSubLineValue(lines[1]);
         UpdateRealVariables();
      }
      if (isFunctionCalled.Count > 0)
      {
         ManageFunctions();
      }
   }

   void InitializeIsFunctionCalled()
   {
      for (int i = 0; i < functionNames.Count; i++)
      {
         isFunctionCalled.Add(false);
      }
   }

   public void UIRealReference(GameObject newUI)
   {
      hackingUI = newUI;
      GetLines();
   }

   public void GetLines()
   {
      foreach (Transform t in hackingUI.GetComponentsInChildren<Transform>())
      {
         if (t.tag == "Line")
         {
            lines.Add(t.gameObject);
         }
      }
   }

   public void CallFunction(string tag)
   {
      if (tag == functionNames[0])
      {
         isFunctionCalled[0] = true;
      }
   }

   public void CreateFunctions()
   {
      hackingUI.GetComponent<LineManager>().CreateFunction(functionNames, this, hackingUI);
   }

/*
 * public void ReturnFunctionList()
 * {
 *    hackingUI.GetComponent <LineManager>().functions = functions;
 * }
 */
   public bool ValidReqs(int funcIndex)
   {
      GameObject functionObject = hackingUI.GetComponent<LineManager>().functions[funcIndex];
      Function   function       = functionObject.GetComponent<Function>();

      List<string> reqs    = function.ReturnReq();
      List<string> reqVals = function.ReturnReqVal();



      if ((reqs.Count == 0) || (reqVals.Count == 0))
      {
         return true;
      }
      for (int i = 0; i < reqs.Count; i++)
      {
         string req    = reqs[i];
         string reqVal = reqVals[i];

         foreach (GameObject subLine in lines)
         {
            Line lCode = subLine.GetComponent<Line>();
            if (lCode.tag == req)
            {
               if (lCode.subBoxText.text != reqVal)
               {
                  return(false);
               }
            }
         }
      }

      return(true);
   }

   public virtual void ManageFunctions()
   {
      throw new Exception("Unimplemented Method: ManageFunctions");

      /*
       * if(isFunctionCalled[0]){
       *  PlayerDetected();
       *  }*/
   }

   //Virtual
   public virtual void UpdateRealVariables()
   {
      throw new Exception("Unimplemented Method: UpdateRealVariables");
   }

   //Virtual
   public virtual void SetCorrectSubLineValue()
   {
      throw new Exception("Unimplemented Method: SetCorrectSubLineValue");
   }

   void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Player")
      {
         UIController ui = other.gameObject.GetComponent<UIController>();
         ui.nearInteractable = true;
         ui.interactable     = this;
      }
   }

   void OnTriggerExit(Collider other)
   {
      if (other.tag == "Player")
      {
         UIController ui = other.gameObject.GetComponent<UIController>();
         ui.nearInteractable = false;
         ui.interactable     = null;
      }
   }
}
