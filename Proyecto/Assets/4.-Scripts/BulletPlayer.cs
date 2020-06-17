using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [HideInInspector]
    public GameObject Player;
    public float speedX, speedZ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.Find("Player");
        transform.position = new Vector3(transform.position.x + speedX * Time.deltaTime * 5, Player.transform.position.y, transform.position.z + speedZ * Time.deltaTime * 5);
    }
}
