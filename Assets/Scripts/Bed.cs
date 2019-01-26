using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Damage")) 
        {
            //Destroy(this.gameObject);
            other.gameObject.tag = "Player";
            other.gameObject.GetComponent<Renderer>().material.color = Color.black;     

            Debug.Log("You die");
        }
    }


}
