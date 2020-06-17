using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet2v2 : MonoBehaviour
{
    public enemy2v2 torreta;
    [HideInInspector]
    public GameObject Player;
    public float speedX, speedY;
    float timer;
    float radius = 4f;

    void Update()
    {
        transform.position = new Vector3(transform.position.x + speedX * Time.deltaTime,Player.transform.position.y ,transform.position.z + speedY * Time.deltaTime );
        Player = GameObject.Find("Player");
        //print(Vector3.Distance(transform.position, Player.transform.position));

        timer += Time.deltaTime;
        if(timer >= 1)
        {
            if(Vector3.Distance(transform.position, Player.transform.position) <= radius)
            {
                
                Destroy(Player.gameObject);
                Destroy(gameObject);

            }
            Destroy(gameObject);
        }

    }

    private void OnDrawGizmos()
    {
        if (timer >= 3)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);

        }
    }




}
