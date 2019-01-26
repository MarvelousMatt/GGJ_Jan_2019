using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour {

    public GameObject[] itemToPlace;
    public GameObject[] holograms;

    Camera cam;

    Ray ray;

    RaycastHit hit;


    public Vector3 placementOffset;

    public int itemIndex = 0;

    GameObject currentHolo;

    public Vector3 holoPos;

    Material mat;

    public bool canPlace = false;

    private void Awake()
    {
        cam = Camera.main;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(currentHolo != null)
            mat = currentHolo.GetComponent<Renderer>().material;


        ItemHover();

        if (Input.GetMouseButtonDown(0))
            PlaceItem();
	}

    void ItemHover()
    {

        ray = cam.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Overlay"))
        {
            holoPos = new Vector3(hit.point.x + placementOffset.x, hit.point.y + placementOffset.y, hit.point.z + placementOffset.z);

        }
        else if (currentHolo != null)
            Destroy(currentHolo);

       

        if (Physics.Raycast(ray, out hit) && currentHolo == null)
        {
            if (hit.collider.gameObject.CompareTag("Overlay"))//Probably more conditions here later
            {
                currentHolo = Instantiate(holograms[itemIndex], holoPos, Quaternion.identity);
            }
        }

        if(currentHolo != null)
        {
            currentHolo.transform.position = holoPos;

            Collider[] foundCols = Physics.OverlapBox(holoPos, currentHolo.GetComponent<Renderer>().bounds.extents);

            if(mat != null)
            {
                mat.color = Color.green;
                //placementOffset.z += 0.1f;
            }
                

            canPlace = true;


            for (int i = 0; i < foundCols.Length; i++)
            {
                if (foundCols[i].gameObject.CompareTag("Prop") || foundCols[i].gameObject.CompareTag("Floor"))
                {
                    mat.color = Color.red;
                    //placementOffset.z -= 0.1f;
                    canPlace = false;
                }
            }

            


        }

    }

    void PlaceItem()
    { 

        if (currentHolo != null && canPlace)
        {
            Instantiate(itemToPlace[itemIndex], holoPos, Quaternion.identity);
        }
    }



}
