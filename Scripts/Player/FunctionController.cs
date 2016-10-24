using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FunctionController : MonoBehaviour
{

/*
   Handles the creation of functions and the controls and application of them within the UI


   @type UIController: uiController
        Reference to the UIController
   @type int: activeFunction
        the currently highlighted function in the functions menu
   @type sprite: HighlightedFunction
        The sprite of the highlighted functions
   @type Sprite: previousFunctionSprite
        The previous function sprite before being selected

 */
private UIController uiController;
private int activeFunction = 0;
//private List <GameObject> functions;
public Sprite HighlightedFunction;
private Sprite previousFunctionSprite;


void Start()
{
        uiController = GetComponent <UIController>();
}
/*
  Handles user input to control the function menu
*/
public void FunctionMenuControls()
{
        if (uiController.hasTutorial)
        {
                Tutorial tut = uiController.tutorial.GetComponent<Tutorial>();
                TutorialProperties tutProp = tut.tutorials[tut.index].GetComponent<TutorialProperties>();
                if (!(!uiController.tutorial.activeSelf || (uiController.tutorial.activeSelf && tutProp.AllowInventoryControls)))
                {
                        return;
                }
        }



        if (!uiController.YAxisAccessed) {
                if(Input.GetAxis("Left Stick Y Axis") <= -0.5) {
                        uiController.YAxisAccessed = true;
                        ChangeHighlightedFunction(-1);
                        UpdateHighlightedFunction();
                }
                else if(Input.GetAxis("Left Stick Y Axis") >= 0.5) {
                        uiController.YAxisAccessed = true;
                        ChangeHighlightedFunction(1);
                        UpdateHighlightedFunction();
                }
        }
        if(Input.GetButtonDown("B_Button")) {
                uiController.activeLineManager.functions[activeFunction].GetComponent <Image>().sprite = previousFunctionSprite;
                uiController.functionMenuInteraction = false;
        }
        if(Input.GetButtonDown("A_Button") && previousFunctionSprite != null) {
                string newText = uiController.activeLineManager.functions[activeFunction].GetComponent <Function>().tag;
                Line line    = uiController.activeLineManager.lines[uiController.ReturnActiveLine()].GetComponent <Line>();
                line.AddToText(newText);
                Function function = uiController.activeLineManager.functions[activeFunction].GetComponent <Function>();
                function.line = line;
                function.parent.CallFunction(newText);
                uiController.activeLineManager.functions[activeFunction].GetComponent <Image>().sprite = previousFunctionSprite;
                uiController.functionMenuInteraction = false;
        }
}

/*
calculates the index of the highlighted function
*/
void ChangeHighlightedFunction(int value)
{
        if(previousFunctionSprite != null) {
                Image lineImage = uiController.activeLineManager.functions[activeFunction].GetComponent <Image>();
                lineImage.sprite = previousFunctionSprite;
        }
        if(uiController.activeLineManager.functions.Count == 1) {
                return;
        }
        if(activeFunction + value > uiController.activeLineManager.functions.Count - 1) {
                activeFunction = 0;
        }
        else if(activeFunction + value < 0) {
                activeFunction = uiController.activeLineManager.functions.Count - 1;
        }
        else{
                activeFunction += 1;
        }
}

/*
 changes the sprite of the highlighted function and the previous highlighted function appropriately
*/
public void UpdateHighlightedFunction()
{
        print(activeFunction);
        Image functionImage = uiController.activeLineManager.functions[activeFunction].GetComponent <Image>();

        previousFunctionSprite = functionImage.sprite;
        functionImage.sprite   = HighlightedFunction;
}
}
