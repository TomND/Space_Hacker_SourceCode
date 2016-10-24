using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LineManager : MonoBehaviour
{
   /* manages all lines for one code snippet
    *
    *
    */

   public List <GameObject> lines = new List <GameObject>();
   public List <GameObject> functions = new List<GameObject>();

   // Use this for initialization
   void Start()
   {
   }

   // Update is called once per frame
   void Update()
   {
    }



   public void CreateLine(bool isEditable, bool subButton, string text, List<string> reqs, List<string> reqvals, int lineNumber = 0, int type = 0, string tag = null)
   {
      GameObject lineObject = (GameObject)Instantiate(Resources.Load("Line"));
      Line       line       = lineObject.GetComponent <Line>();

      line.editable = isEditable;
      line.tag      = tag;
      line.Initialize();
      line.lineNumber = lines.Count;
      print(isEditable + " is it editable?");

      line.SetText(text);
      if(subButton){
         line.CreateSubButton(lineNumber, type);
        }
        /*
      if(req != null){
            line.SetupReq(req, reqVal);
        }*/
      if(reqs.Count >= 1){
        for(int i = 0; i < reqs.Count;i++){
          line.SetupReq(reqs[i],reqvals[i]);
        }
      }
      lineObject.transform.parent = transform;
      lineObject.GetComponent <RectTransform>().position = new Vector3(-105, 165 - (20 * line.lineNumber), 0);
      lines.Add(lineObject);
   }

   public void CreateFunction(List<string> functionNames, HackingConsole parent, GameObject hackingUI)
    {
        for(int i = 0; i < functionNames.Count; i++){
          GameObject function = (GameObject)Instantiate(Resources.Load("Function"), new Vector3(230, 100 - (10 * i), 0), Quaternion.identity);
            function.GetComponent<RectTransform>().position = new Vector3(230, 100 - (10 * i), 0) * 0.01f;
          function.GetComponent <Function>().tag        = functionNames[i];
          function.GetComponent <Function>().parent     = parent;
          function.GetComponentInChildren <Text>().text = functionNames[i];
            function.transform.parent = hackingUI.transform;
            print(function);
            functions.Add(function);
          }
   }
}
