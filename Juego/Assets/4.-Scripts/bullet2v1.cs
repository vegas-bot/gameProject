using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet2v1 : MonoBehaviour
{
    public enemy2v1 torreta;
    public float speedX, speedZ;
    //[HideInInspector]
    public GameObject Player;

    void Update()
    {
        Player = GameObject.Find("Player");
        transform.position = new Vector3(transform.position.x + speedX * Time.deltaTime, torreta.transform.position.y + 3.25f, transform.position.z + speedZ * Time.deltaTime ); 
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
