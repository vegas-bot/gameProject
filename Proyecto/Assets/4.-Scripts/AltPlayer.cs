using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltPlayer : MonoBehaviour
{
    public float speed = 2f;

    void Start()
    {
        
    }

    void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal") * speed;
        float dirZ = Input.GetAxisRaw("Vertical") * speed;
        transform.position -= new Vector3(dirX, 0, dirZ) * Time.deltaTime;
    }
}
