using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slow : MonoBehaviour
{
    public Vector3 size;

    public GameObject player;

    float Ospeed;

    void Start()
    {
        Ospeed = player.GetComponent<PlayerMovement>().speed;
    }

    void Update()
    {

        if(player.transform.position.x > transform.position.x - size.x/2 && player.transform.position.x < transform.position.x + size.x/2 &&
            player.transform.position.z > transform.position.z - size.z/2 && player.transform.position.z < transform.position.z + size.z/2)
        {
            float Nspeed = (Ospeed * 50) / 100;
            player.GetComponent<PlayerMovement>().speed = Ospeed - Nspeed;
        }
        else
        {
            player.GetComponent<PlayerMovement>().speed = Ospeed;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size);


    }
}
