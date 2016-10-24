using UnityEngine;
using UnityEditor;
using System.Collections;

public class HackableCodeCreator : EditorWindow
{
   private string input = null;
    private string prefabName = null;
   // Add menu item named "My Window" to the Window menu
   [MenuItem("Window/Hackable Code Generator")]
   public static void ShowWindow()
   {
      //Show existing window instance. If one doesn't exist, make one.
      EditorWindow.GetWindow(typeof(HackableCodeCreator));
   }

   void OnGUI()
   {
      GUILayout.Label("Base Settings", EditorStyles.boldLabel);
      input = EditorGUILayout.TextField("File Name", input);
        prefabName = EditorGUILayout.TextField("Prefab Name", prefabName);

        GameObject obj = (GameObject)EditorGUILayout.ObjectField("Line Parent", null, typeof(GameObject), true);
      if(GUILayout.Button("Create Interactive Code")){
         ManageFile(input, obj,prefabName);
         }
   }

   void ManageFile(string fileName, GameObject parent,string preName)
   {
      //FileReader.SetLineParent(parent);
      FileReader.SetFileName(fileName);
      FileReader.SetStreamReader();
      FileReader.CreatePrefabTemplate();
      FileReader.ReadFile();
        FileReader.SavePrefab(preName);
        /*
        FileReader.CreatePrefabTemplate();
        FileReader.CreateLine(true, true, "int speed = ",12,0);
        FileReader.CreateLine(true, false, "swivel(speed)");
        FileReader.CreateLine(true, false, "if (EnemySpotted()){");
        FileReader.CreateLine(true, false, "SoundAlarm()");
        FileReader.CreateLine(true, false, "}");
        FileReader.SavePrefab();
        */
    }
}
