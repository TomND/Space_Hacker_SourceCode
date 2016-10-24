using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
    public float speed;
    public bool fromEnemy;
    // Use this for initialization
    void Start()
    {
        if (fromEnemy)
        {
            Vector3 rand = new Vector3(Random.Range(300, 800), 0, Random.Range(300, 800));
            GetComponent<Rigidbody>().AddForce(Vector3.up * 900 + rand);
        }

	}

	// Update is called once per frame
	void Update () {
		transform.eulerAngles += new Vector3(0, speed, 0);
    }



    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            Destroy(gameObject);
        }
  }
}
