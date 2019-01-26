using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingObject : MonoBehaviour {



    public bool isParent = false;
    public bool isMegaParent = false;

    private void Awake()
    {
        gameObject.tag = "Damage";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Projectile"))
        {
            if (isMegaParent)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    for (int m = 0; m < transform.GetChild(i).childCount; m++)
                    {
                        Rigidbody rigid = transform.GetChild(i).GetChild(m).gameObject.AddComponent<Rigidbody>();
                        rigid.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                        rigid.AddForce(Vector3.down * 1000);
                        transform.GetChild(i).GetChild(m).gameObject.GetComponent<Renderer>().material.color = Color.black;
                        transform.GetChild(i).GetChild(m).gameObject.tag = "Player";
                    }

                    transform.GetChild(i).DetachChildren();

                }

                transform.DetachChildren();
                Destroy(gameObject);
            }

            if (isParent)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Rigidbody rigid = transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
                    rigid.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                    rigid.AddForce(Vector3.down * 1000);
                    transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = Color.black;
                    transform.GetChild(i).gameObject.tag = "Player";
                }

                transform.DetachChildren();

                Destroy(gameObject);
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.collider.gameObject.CompareTag("Projectile"))
        {
            if (isMegaParent)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    for (int m = 0; m < transform.GetChild(i).childCount; m++)
                    {
                        Rigidbody rigid = transform.GetChild(i).GetChild(m).gameObject.AddComponent<Rigidbody>();
                        rigid.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                        rigid.AddForce(Vector3.down * 1000);
                        transform.GetChild(i).GetChild(m).gameObject.GetComponent<Renderer>().material.color = Color.black;
                        transform.GetChild(i).GetChild(m).gameObject.tag = "Player";
                    }

                    transform.GetChild(i).DetachChildren();

                }

                transform.DetachChildren();
                Destroy(gameObject);
            }

            if (isParent)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Rigidbody rigid = transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
                    rigid.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                    rigid.AddForce(Vector3.down * 1000);
                    transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = Color.black;
                    transform.GetChild(i).gameObject.tag = "Player";
                }

                transform.DetachChildren();

                Destroy(gameObject);
            }
        }
    }


        
}
