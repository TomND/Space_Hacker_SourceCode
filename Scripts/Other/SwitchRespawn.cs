using UnityEngine;
using System.Collections;

public class SwitchRespawn : MonoBehaviour {


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GetComponentInParent<Respawner>().respawnPoint = gameObject;
        }
    }

}
