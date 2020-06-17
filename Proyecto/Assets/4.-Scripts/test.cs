using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Vector3 worldPosition;
    public Plane piso;


    void Start()
    {
        
    }

    void Update()
    {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;

            if (Physics.Raycast(ray, out hitData, 1000) && hitData.transform.tag == "Floor")
            {
                worldPosition = hitData.point;
            }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(worldPosition,1f);
    }
}
