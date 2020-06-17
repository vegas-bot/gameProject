using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    float speedX, speedZ;

    void Start()
    {
        
    }

    void Update()
    {
        speedX = 30 * Input.GetAxis("Horizontal");
        speedZ = 30 * Input.GetAxis("Vertical");

        transform.position = new Vector3(transform.position.x + speedX * Time.deltaTime, transform.position.y, transform.position.z + speedZ * Time.deltaTime);
    }
}
