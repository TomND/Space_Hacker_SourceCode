using UnityEngine;
using System.Collections;

public class Triggered : MonoBehaviour {

    public Tutorial tutorial;
    public bool noTutorial;
    public bool setLine;
    public int lineNum;
    public bool enablesJumping;
    public bool enablesRolling;

    // Use this for initialization
    void Start () {

	}

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            if (enablesJumping)
            {
                PlayerControllerThirdPersonVR cont = other.gameObject.GetComponent<PlayerControllerThirdPersonVR>();
                cont.jumpEnabled = true;
            }
            else if (enablesRolling)
            {
                PlayerControllerThirdPersonVR cont = other.gameObject.GetComponent<PlayerControllerThirdPersonVR>();
                cont.rollEnabled = true;
            }
        }

        if (setLine)
        {
            if(other.tag == "Player")
            {
                LineController controller = other.gameObject.GetComponent<LineController>();
                controller.activeLine = lineNum;
            }
        }
        if (noTutorial)
        {
            return;
        }
       if(other.tag == "Player" && !tutorial.startTutorial){
            tutorial.player = other.gameObject.GetComponent<PlayerControllerThirdPersonVR>();
            tutorial.player.activeTutorial = true;
            tutorial.startTutorial = true;
          tutorial.tutorials[tutorial.index].SetActive(true);
          }
    }
}
