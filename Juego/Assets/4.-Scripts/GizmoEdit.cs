using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoEdit : MonoBehaviour
{
    public float radius;
    public float height;
    public float width;

    public bool sphere = true;

    void OnDrawGizmos()
    {
        if(sphere)
        {
            Gizmos.DrawWireSphere(transform.position, radius);
            return;
        }
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
}

