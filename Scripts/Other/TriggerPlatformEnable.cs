using UnityEngine;
using System.Collections;

public class TriggerPlatformEnable : MonoBehaviour {


	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			GetComponent<MovingPlatform>().enabled = true;
		}
	}

}
