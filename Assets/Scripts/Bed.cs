using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour {

    public int health;
    public int maxHealth;

    GameManager man;

    // Use this for initialization
    void Start ()
    {
       health =  maxHealth;
        man = GameObject.FindGameObjectWithTag("Respawn").GetComponent<GameManager>();
	}
	

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Projectile"))
        {
            health -= 1; 

            if(health < 1)
            {
                Debug.Log("You died");
                man.StopAllCoroutines();
                man.waveStarted = false;
                man.ButtonCall(true);

            }
        }

        
    }


}
