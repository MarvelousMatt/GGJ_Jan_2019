using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dreamcatcher : MonoBehaviour {

    int counter;
    public int max;

	// Update is called once per frame
	void Update () {


        counter++;
        
        if (counter == max)
        {
            counter = 0;

            GetComponent<Light>().enabled = true;
            Invoke("lightoff", 0.5f);

            Collider[] cols = Physics.OverlapSphere(transform.position,10);

            foreach (Collider col in cols)
            {
                try
                {
                    if(col.gameObject != gameObject && !col.gameObject.CompareTag("Prop"))
                        col.gameObject.GetComponent<Rigidbody>().AddExplosionForce(1000,transform.position,10);
                }
                catch
                {

                }
            }

            


        }
        
	}

    void lightoff()
    {
        GetComponent<Light>().enabled = false;
    }
}
