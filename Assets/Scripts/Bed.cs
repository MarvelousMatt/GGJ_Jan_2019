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
        //Debug.Log("Dwa");

        if (other.gameObject.CompareTag("Projectile")) 
        {
            //Destroy(other.gameObject);

            //for (int i = 0; i < other.gameObject.transform.parent.childCount; i++)
            //{
            //    other.gameObject.transform.GetChild(i).tag = "Player";
            //}

            
            Debug.Log("You die");
        }
    }


}
