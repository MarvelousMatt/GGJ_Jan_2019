using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour {

    public int health;
    public int maxHealth;

    private void Awake()
    {
        health = maxHealth;
    }

    public void PropTakeDamage(int amount)
    {
        Debug.Log("OOF");

        health = health - amount;
        if (health < 1)
        {
            Destroy(gameObject);
        }
    }


  

}
