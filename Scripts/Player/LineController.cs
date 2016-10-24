using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LineController : MonoBehaviour
{
/*
 * handles each line of code inside a hacking console UI
 *
 * @type UIController: uiController
 *      Reference to the uiController object
 * @type Sprite: highlightedLine
 *      sprite of the highlighted line
 * @type Sprite: previousLineSprite
 *      Previous sprite of the highlighted line
 * @type int: activeLine
 *      the index of the activeLine
 * @type GameObject: activeLineObject
 *      The GameObject of the activeLineObject
 * @type Line lineCode:
 *      Reference to the line object for the active line
 *
 */

   private UIController uiController;
   public Sprite        highlightedLine;
   private Sprite       previousLineSprite;
   public int           activeLine;
   public GameObject    activeLineObject;
   private Line         lineCode;



   void Start()
   {
      uiController = GetComponent<UIController>();
   }

/*
 * Handles userinput to control the selection and general controls for the line manager.
 */
   public void LineControls()
   {
      if (uiController.hasTutorial)
      {
         Tutorial           tut     = uiController.tutorial.GetComponent<Tutorial>();
         TutorialProperties tutProp = tut.tutorials[tut.index].GetComponent<TutorialProperties>();

         /*if (!(!uiController.tutorial.activeSelf || (uiController.tutorial.activeSelf && tutProp.AllowInventoryControls)))
          * {
          *      return;
          * }*/
         if (uiController.tutorial.activeSelf && !tutProp.AllowInventoryControls)
         {
            return;
         }
      }


      if (!uiController.YAxisAccessed)
      {
         if (Input.GetAxis("Left Stick Y Axis") <= -0.5 || Input.GetAxis("DPad Y Axis")  >= 0.5)
         {
            uiController.YAxisAccessed = true;
            ChangeHighlightedLine(-1);
            UpdateHighlightedLine();
         }
         else if (Input.GetAxis("Left Stick Y Axis") >= 0.5 || Input.GetAxis("DPad Y Axis")  <= -0.5)
         {
            uiController.YAxisAccessed = true;
            ChangeHighlightedLine(1);
            UpdateHighlightedLine();
         }
      }

      if (Input.GetButtonDown("A_Button"))
      {
         lineCode = activeLineObject.GetComponent<Line>();

         if (lineCode.subObject != null)
         {
            lineCode.subObject.GetComponent<Text>().color = Color.yellow;
            uiController.subObjectInteraction             = true; //// = 1;
         }
         else if (uiController.FunctionExists())
         {
            //function menu //TODO: Change this
            print(lineCode.GetText().Trim().Length);
            if (lineCode.GetText().Trim().Length <= 2)
            {
               uiController.functionMenuInteraction = true;
               uiController.UpdateHighlightedFunction();
            }
         }
      }

      if (Input.GetButtonDown("B_Button"))
      {
         Image lineImage = uiController.activeLineManager.lines[activeLine].GetComponent<Image>();
         lineImage.sprite = previousLineSprite;
         uiController.activeInterface.SetActive(false);
         uiController.HackingUIParent.SetActive(false);
         uiController.activeInventory = false;
         if (uiController.tutorial.GetComponent<Tutorial>().disableAtEnd)
         {
            uiController.tutorial.SetActive(false);
         }
         activeLine       = 0;
         lineImage        = uiController.activeLineManager.lines[activeLine].GetComponent<Image>();
         lineImage.sprite = previousLineSprite;
      }
   }

/*
 * handles unique controls for each line, such ass changing values and dealing with variables, etc.
 */
   public void LineSpecialControls()
   {
      if (!uiController.YAxisAccessed)
      {
         if (Input.GetButtonDown("B_Button"))
         {
            lineCode.subObject.GetComponent<Text>().color = Color.white;
            uiController.subObjectInteraction             = false;
         }



         if (((lineCode.GetSubText() == "False") || (lineCode.GetSubText() == "false")) && (Mathf.Abs(Input.GetAxis("Left Stick Y Axis")) >= 0.5 || Mathf.Abs(Input.GetAxis("DPad Y Axis"))  >= 0.5))
         {
            uiController.YAxisAccessed = true;
            lineCode.SetSubButtonText("true");
            return;
         }
         else if (((lineCode.GetSubText() == "True") || (lineCode.GetSubText() == "true")) && (Mathf.Abs(Input.GetAxis("Left Stick Y Axis")) >= 0.5 || Mathf.Abs(Input.GetAxis("DPad Y Axis"))  >= 0.5))
         {
            uiController.YAxisAccessed = true;
            lineCode.SetSubButtonText("false");
            return;
         }



         if (Input.GetAxis("Left Stick Y Axis") <= -0.5f || Input.GetAxis("DPad Y Axis")  >= 0.5f)
         {
            uiController.YAxisAccessed = true;
            lineCode.AddValueToSubButtonText(1);
         }
         else if (Input.GetAxis("Left Stick Y Axis") >= 0.5f || Input.GetAxis("DPad Y Axis")  <= -0.5f)
         {
            uiController.YAxisAccessed = true;
            lineCode.AddValueToSubButtonText(-1);
         }
      }
   }

/*
 * calcualtes and changes the value of activeLine
 */
   void ChangeHighlightedLine(int value)
   {
      if (previousLineSprite != null)
      {
         Image lineImage = uiController.activeLineManager.lines[activeLine].GetComponent<Image>();
         lineImage.sprite = previousLineSprite;
      }
      do
      {
         if (activeLine + value > uiController.activeLineManager.lines.Count - 1)
         {
            activeLine = 0;
            if (uiController.activeLineManager.lines[activeLine].GetComponent<Line>().GetEditableValue() == false)
            {
               activeLine += 1;
            }
         }
         else if (activeLine + value < 0)
         {
            activeLine = uiController.activeLineManager.lines.Count - 1;
            if (uiController.activeLineManager.lines[activeLine].GetComponent<Line>().GetEditableValue() == false)
            {
               activeLine -= 1;
            }
         }
         else
         {
            activeLine += value;
         }
      } while (uiController.activeLineManager.lines[activeLine].GetComponent<Line>().GetEditableValue() == false);
   }

/*
 * updates the sprite of the highlightedline and previously active line appropriately
 */
   public void UpdateHighlightedLine()
   {
      Image lineImage = uiController.activeLineManager.lines[activeLine].GetComponent<Image>();

      previousLineSprite = lineImage.sprite;
      lineImage.sprite   = highlightedLine;
      activeLineObject   = uiController.activeLineManager.lines[activeLine];
   }
}
