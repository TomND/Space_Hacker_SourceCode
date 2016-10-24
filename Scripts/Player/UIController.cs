using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
/* This class manages when the UI gets opened and the controls inside the UI

   @type GameObject: tutorial
        Gameobject of tutorial attached to UI, if one exists
   @type bool: createTutorial
        createstutorial if true
   @type bool: hasTutorial
        indicated whether the UI has a tutorial attached
   @type LineController: lineController
        Reference to lineController object
   @type FunctionController: functionController
        Reference to FunctionController object
   @type GameObject: activeInterface
        Reference to the currently active UI hacking interface
   @type LineManager: activeLineManager
        Reference to LineManager object of the currently active hacking interface
   @type GameObject: HackingUIParent
        parent of the active hacking UI
   @type GameObject: interactionNotification
        Reference to the interaction notification that appears when near a hacking console.
   @type bool: activeInventory
        true if there is currently an active inventory
   @type PlayerController: pController
        Reference to the player controller object
   @type bool: subObjectInteraction
        true if a sub object interaction is occuring, such as editing variables.
   @type bool: functionMenuInteraction
        true if the function menu is being interacted within
   @type int: activeFunction
        the index of the currently active function
   @type List<GameObject>: functions
        A list of all functions in the active inventory
   @type Sprite: HighlightedFunction
        sprite used for highlighted functions
   @type Sprite: previousFunctionSprite
        the sprite of a function before it becomes a highlighted function
   @type bool: nearInteractable
        true if player is near an interactable object
   @type HackingConsole: interactable
        reference to HackingConsole object of the specific interactable
   @type bool: YAxisAccessed
        becomes true when the left joysticks Y-axis is changed

 */
public GameObject tutorial;
public bool createTutorial;
public bool hasTutorial;
private LineController lineController;
private FunctionController functionController;

public GameObject activeInterface;
public LineManager activeLineManager;
public GameObject HackingUIParent;            // parent of the UI
public GameObject interactionNotification;
public bool activeInventory;
private PlayerController pController;
//Line Variables

//Line Variables
public bool subObjectInteraction;
public bool functionMenuInteraction;
//Function Variables

private int activeFunction = 0;
private List <GameObject> functions;
public Sprite HighlightedFunction;
private Sprite previousFunctionSprite;
//Function Variables
[HideInInspector]
public bool nearInteractable;
public HackingConsole interactable;

public bool YAxisAccessed;

public int ReturnActiveLine()
{
        return(lineController.activeLine);
}

// Use this for initialization
void Start()
{
        lineController     = GetComponent <LineController>();
        functionController = GetComponent <FunctionController>();

        pController = GetComponent <PlayerController>();
}

// Update is called once per frame
void Update()
{
        if(activeInventory) {
                NewManageInventory();
        }
        else{
                CheckForInteractable();
        }
}

/*
   Handles which inventory controls to call. depending on what the player is accessing in the inventory.
 */
void NewManageInventory()
{
        ResetYAxisPressed();
        if(subObjectInteraction) {
                lineController.LineSpecialControls();
        }
        else if(functionMenuInteraction) {
                functionController.FunctionMenuControls();
        }
        else{
                lineController.LineControls();
        }
}

/*
  Calls functionController.UpdateHighlightedFunction()
*/
public void UpdateHighlightedFunction()
{
        functionController.UpdateHighlightedFunction();
}

/*
 sets YAxis pressed to false once the y axis is 0
*/
public void ResetYAxisPressed()
{
        if(Input.GetAxis("Left Stick Y Axis") <= 0.4f && Input.GetAxis("Left Stick Y Axis") >= -0.4f && Input.GetAxis("DPad Y Axis")  == 0) {
                YAxisAccessed = false;
        }
}

public bool ActiveInventory()
{
        return(activeInventory);
}

/* Returns true if atleast one function exists*/
public bool FunctionExists()
{
        if(activeLineManager.functions.Count >= 1) {
                return(true);
        }
        else{
                return(false);
        }
}


//old
void CheckForInteractableOld()
{
        RaycastHit hit;
        Camera camera = GetComponentInChildren <Camera>();
        Ray ray    = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if(Physics.Raycast(ray, out hit, 2)) {
                if(hit.transform.tag == "Interactable") {
                        interactionNotification.SetActive(true);
                        if(Input.GetButtonDown("X_Button") && !activeInventory) {
                                HackingConsole console = hit.transform.gameObject.GetComponent <HackingConsole>();
                                AccessedNewInterface(console);
                                activeInventory = true;
                        }
                }
                else{
                        interactionNotification.SetActive(false);
                }
        }
        else{
                interactionNotification.SetActive(false);
        }
        //if(Physics.Raycast(cameraObject.transform.position, cameraObject.transform.forward, float maxDistance, int layerMask))
}

/*
  checks if near interactable and if player presses X, prepares new active inventory

*/

void CheckForInteractable()
{


        if (nearInteractable)
        {
                interactionNotification.SetActive(true);
                if (Input.GetButtonDown("X_Button") && !activeInventory)
                {
                        AccessedNewInterface(interactable);
                        activeInventory = true;
                }

        }
        else
        {
                interactionNotification.SetActive(false);
        }
        //if(Physics.Raycast(cameraObject.transform.position, cameraObject.transform.forward, float maxDistance, int layerMask))
}

/*
 calls all methods and sets all variables when a new interface is accessed.
*/
public void AccessedNewInterface(HackingConsole console)
{
        GameObject newUI;

        if(console.hackingUI != null) {
                newUI = console.hackingUI;
                newUI.SetActive(true);
        }
        else{
                newUI = (GameObject)Instantiate(console.hackingUIPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                console.UIRealReference(newUI);
                console.GetLines();
                console.SetCorrectSubLineValue();
                console.CreateFunctions();
                hasTutorial = console.hasTutorial;
        }

        //lineController.activeLine = 0;
        SetActiveInterface(newUI);
        activeInterface.transform.parent = HackingUIParent.transform;
        activeInterface.GetComponent <RectTransform>().localPosition = new Vector3(0, 0, 0);
        activeInterface.GetComponent<RectTransform>().eulerAngles = HackingUIParent.transform.eulerAngles;
        activeLineManager = activeInterface.GetComponent <LineManager>();
        lineController.activeLineObject = activeLineManager.lines[lineController.activeLine];
        lineController.UpdateHighlightedLine();
        interactionNotification.SetActive(false);
        HackingUIParent.SetActive(true);
        subObjectInteraction = false;

        /*
           if (createTutorial)
           {

           tutorial.SetActive(true);
           createTutorial = false;
           }*/
        if(console.tutorial != null && !console.tutCalled) {

                console.tutorial.SetActive(true);
                tutorial = console.tutorial;
                createTutorial = false;
                console.tutCalled = true;
        }
}

// sets the activbeinterface to the value of newUI
public void SetActiveInterface(GameObject newUI)
{
        activeInterface = newUI;
}
}
