using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arm : MonoBehaviour {

    LineRenderer line;

    public Vector3 holoPos;

    Ray ray;

    RaycastHit hit;

    public Vector3 placementOffset;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }
    
    public void Update()
    {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Overlay"))
        {
            holoPos = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            line.enabled = true;
        }
        else
        {
            line.enabled = false;
        }

        Vector3[] pos = new Vector3[2];

        pos[1] = transform.position;
        pos[0] = holoPos;

        line.SetPositions(pos);


    }
}
