using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy4 : MonoBehaviour
{
    [HideInInspector]
    public GameObject Player;
    float timer;
    float radius = 4f;
    bool activate;

    void Start()
    {
        activate = false;
    }

    void Update()
    {
        Player = GameObject.Find("Player");
        print(activate);
        if (Vector3.Distance(transform.position, Player.transform.position) <= radius)
        {
            activate = true;
            print("entro");
        }

        if (activate)
        {
            timer += Time.deltaTime;
        }
        if (Vector3.Distance(transform.position, Player.transform.position) <= radius && activate && timer >= 4)
        {
            Destroy(Player.gameObject);
            Destroy(gameObject);
        }
        if (Vector3.Distance(transform.position, Player.transform.position) >= radius && activate && timer >= 4)
        {
            Destroy(gameObject);
        }
    }

     void OnDrawGizmos()
    {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
    }
}
