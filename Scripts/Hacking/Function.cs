using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Function : MonoBehaviour {
   public string         tag;
   public HackingConsole parent;
   [HideInInspector]
    public Line line;


   // Use this for initialization
   void Start()
   {
   }

   /* Checks if the lines requirements to run have been met
    */
   public List<string> ReturnReq()
   {
        return line.reqs;
    }

    public List<string> ReturnReqVal(){
        return line.reqVals;
    }

   // Update is called once per frame
   void Update()
   {
   }
}
