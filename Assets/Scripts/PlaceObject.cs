using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceObject : MonoBehaviour {

    public GameObject[] itemToPlace;
    public GameObject[] holograms;
    public int[] amounts;
    public int[] rounds;

    public Text leftNo;

    Camera cam;

    Ray ray;

    RaycastHit hit;


    public Vector3 placementOffset;

    public int itemIndex = 0;

    GameObject currentHolo;

    public Vector3 holoPos;

    Material mat;

    public bool canPlace = false;

    public GameObject previewBox;

    bool isButton = false;

    int indexSave = 0;


    private void Awake()
    {
        
        cam = Camera.main;
        ResetITemAmounts();
    }

    public void ResetITemAmounts()
    {
        
        
            rounds = (int[])amounts.Clone();
        
       
    }


    public void SwapItem(bool left)
    {
        if (left && itemIndex != 0)
        {
            itemIndex -= 1;
        }
        else if(!left && itemIndex != itemToPlace.Length - 1)
        {
            itemIndex += 1;
        }

        if(currentHolo != null)
        {
            Destroy(currentHolo);
        }

        
        leftNo.text = rounds[itemIndex].ToString();
        
    }

   void NotButton()
    {
        isButton = false;
    }
    

    // Use this for initialization
    void Start () {
        leftNo.text = rounds[itemIndex].ToString();
    }
	
	// Update is called once per frame
	void Update () {

        if(indexSave != itemIndex)
        {
            isButton = true;
            Invoke("NotButton", 0.5f);
        }

      

        if (currentHolo != null)
            mat = currentHolo.GetComponent<Renderer>().material;


        ItemHover();


        if (Input.GetMouseButtonDown(0) && !isButton)
            Invoke("PlaceItem", 0.2f);

        indexSave = itemIndex;
      
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

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentHolo.transform.Rotate(Vector3.forward * 90);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentHolo.transform.Rotate(Vector3.back * 90);
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


        if (currentHolo != null && canPlace && !isButton && rounds[itemIndex] != 0)
        {
            Instantiate(itemToPlace[itemIndex], holoPos, currentHolo.transform.rotation);
            rounds[itemIndex] -= 1;
        }

        leftNo.text = rounds[itemIndex].ToString();

        
    }



}
