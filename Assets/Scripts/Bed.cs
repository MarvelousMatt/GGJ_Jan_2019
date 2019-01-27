using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour {

    public int health;
    public int maxHealth;

    public GameObject image;
    public GameObject text;

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

                image.SetActive(true);
                text.SetActive(true);
                Invoke("deactive", 5);

            }
        }

        
    }

    public void deactive()
    {
        image.SetActive(false);
        text.SetActive(false);
    }


}
