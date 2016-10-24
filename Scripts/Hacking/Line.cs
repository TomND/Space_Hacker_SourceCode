using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Line : MonoBehaviour {
/* Contains all properties for one line object
 *
 */
//private string text;
   private Text      textBox;
   public bool       editable;
   public string     tag;
   public int        lineNumber;
   public Text       subBoxText;
   public GameObject subObject;
   public List<string> reqs = new List<string>();
   public List<string> reqVals = new List<string>();


   public void Initialize()
   {
      textBox = GetComponentInChildren<Text>();
      if (!editable)
      {
         textBox.color = new Color(248f / 255f, 200f / 255f, 200f / 255f);   //Color.cyan;
      }
   }

   public void SetupReq(string theReq, string theReqVal)
   {
      reqs.Add(theReq);
      reqVals.Add(theReqVal);
   }

   public void IsEditable(bool val)
   {
      editable = val;
   }

   public bool GetEditableValue()
   {
      return editable;
   }

   public void CreateSubButton(int lineIndex, int type)
   {
      if (type == 0)//sub text
      {
         subObject = (GameObject)Instantiate(Resources.Load("SubText"));
         subObject.transform.parent = transform;
         print(lineIndex);
         subObject.GetComponent<RectTransform>().offsetMin = new Vector2(6.25f * lineIndex, 0);
         subObject.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);   // = new Vector3(0, 0, 0);//217 + lineNumber * 5.615f
         subBoxText = subObject.GetComponent<Text>();
      }
   }

   public string GetSubText()
   {
      return subBoxText.text;
   }

   public void SetSubButtonText(string newText)
   {
      subBoxText.text = newText;
   }

   public void AddValueToSubButtonText(int value)
   {
      float current = float.Parse(subBoxText.text);

      subBoxText.text = ((current + value).ToString());
   }

   public void SetText(string newText)
   {
      textBox.text = newText;
   }

   public void AddToText(string newText)
   {
      textBox.text += newText;
   }

   public string GetText()
   {
      return textBox.text;
   }

   // Use this for initialization
   void Start()
   {
      textBox = GetComponentInChildren<Text>();
   }

   // Update is called once per frame
   void Update()
   {
   }

   /*void UpdateUIText(){
    * textBox.text = text;
    * }*/
}
