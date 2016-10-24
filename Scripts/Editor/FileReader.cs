using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class FileReader : MonoBehaviour {
   private static string       fileName;
   private static StreamReader fileReader; //= new StreamReader(@"E:\Git_Repositories\Hacker_Game\Assets\Scripts\Editor\code_sample.txt");
   private static GameObject   lineManagerObject;


   /*Deprecated */
   public static void SetLineParent(GameObject parent)
   {
      //lineParent = parent;
   }

   public static void SetFileName(string name)
   {
      print(name);
      fileName = @"E:\Projects\Hacker_Game\Assets\Scripts\Editor\" + name + ".txt";
   }

   public static void SetStreamReader()
   {
      print(fileName);
      fileReader = new StreamReader(fileName);
   }

   public static void ReadFile()
   {
      string line;
      int    lineNumber = 1;

      while((line = fileReader.ReadLine()) != null){
            line = lineNumber.ToString() + " " + line;
            AnalyseLine(line);
            lineNumber += 1;
            }
   }

   static void AnalyseLine(string line)
   {
      bool   isEditable          = false;
      bool   isButton            = false;
      int    subButtonLineNumber = 0;
      int    type   = 0;
      string tag    = null;
      List<string> reqs = new List<string>();
      List<string> reqvals = new List<string>();
      string req    = null;
      string reqVal = null;

      while(line.Contains("$")){
            if(line.Contains("$editable$")){
               int start = line.IndexOf("$editable$", 0);
               int end   = line.IndexOf("$", start + 1);
               isEditable = true;
               line       = line.Remove(start, end - start + 1);
               //string text = "Add your code here";
               }
            if(line.Contains("$button=")){
               int    start    = line.IndexOf("$button=", 0);
               int    end      = line.IndexOf("$", start + 1);
               string tagCheck = line.Substring(start, end - start + 1);
               line                = line.Remove(start, end - start + 1);
               tag                 = tagCheck.Substring(tagCheck.IndexOf("=") + 1);
               tag                 = tag.Remove(tag.Length - 1);
               isButton            = true;
               subButtonLineNumber = start;
               //string text = line.Substring(0, start) + line.Substring(start + 1, end - start - 1) + line.Substring(end + 1, line.Length - end - 1);
               }
            if(line.Contains("$req:")){
               int    start    = line.IndexOf("$req:", 0);
               int    end      = line.IndexOf("$", start + 1);
               string tagCheck = line.Substring(start, end - start + 1);
                line = line.Remove(start, end - start + 1);
                int reqStart = tagCheck.IndexOf(":");
               int reqEnd   = tagCheck.IndexOf("=");
               req    = tagCheck.Substring(reqStart + 1, (reqEnd - 1) - reqStart);
               reqVal = tagCheck.Substring(reqEnd + 1, (tagCheck.Length - 1) - (reqEnd + 1));
               reqs.Add(req);
               reqvals.Add(reqVal);
               subButtonLineNumber = start + req.Length;
            }

            }
      CreateLine(isEditable, isButton, line, reqs, reqvals, subButtonLineNumber, type, tag);
   }

   /*
    * void AnalyseCommand()
    * {
    * }*/

   public static void CreatePrefabTemplate()
   {
      lineManagerObject = new GameObject();  //(GameObject)Resources.Load("LineManager");   // create template
      lineManagerObject.AddComponent <LineManager>();
      lineManagerObject.AddComponent <RectTransform>();
   }

   public static void CreateLine(bool isEditable, bool subButton, string text, List<string> reqs, List<string> reqvals,int lineNumber = 0, int type = 0, string tag = null)
   {
      LineManager lineManager = lineManagerObject.GetComponent <LineManager>();

      lineManager.CreateLine(isEditable, subButton, text, reqs, reqvals, lineNumber, type, tag);
   }

   public static void SavePrefab(string name)
   {
      string path = GetPrefabPath(name);

      PrefabUtility.CreatePrefab(path, lineManagerObject);
      Object.DestroyImmediate(lineManagerObject, true);
   }

   private static string GetPrefabPath(string name)
   {
      string path = Path.GetFileNameWithoutExtension(name);

      return("Assets/Prefabs/CreatedInEditor/" + path + ".prefab");
   }
}
