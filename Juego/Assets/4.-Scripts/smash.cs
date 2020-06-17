using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smash : MonoBehaviour
{
    public Vector3 size;

    public GameObject player;

    public bool push;

    private bool move;

    private bool close;

    public bool automatic;

    private float timer;

    public GameObject[] walls;

    public float speed;

    void Start()
    {
        push = false;
        walls[0].transform.position = new Vector3(walls[0].transform.position.x, transform.localPosition.y + ((size.y / 2) - (walls[0].transform.localScale.y / 2)), walls[0].transform.position.z);
        walls[1].transform.position = new Vector3(walls[1].transform.position.x, transform.localPosition.y - ((size.y / 2) - (walls[1].transform.localScale.y / 2)), walls[1].transform.position.z); 
    }

    void Update()
    {
        walls[0].transform.localScale = new Vector3(size.x, walls[0].transform.localScale.y, walls[0].transform.localScale.z);
        walls[1].transform.localScale = new Vector3(size.x, walls[1].transform.localScale.y, walls[1].transform.localScale.z);

        if (walls[0].transform.position.y == (transform.localPosition.y + ((size.y / 2) - (walls[0].transform.localScale.y / 2))))
        {
            print("arriba");
            close = true;
        }

        else if (walls[0].transform.position.y == (transform.localPosition.y + (walls[0].transform.localScale.y / 2)))
        {
            print("abajo");
            close = false;
        }

        if (!automatic)
        {
            if (player.transform.position.x > transform.position.x - size.x / 2 && player.transform.position.x < transform.position.x + size.x / 2 &&
                player.transform.position.y > transform.position.y - size.y / 2 && player.transform.position.y < transform.position.y + size.y / 2)
            {
                print("entro");
                push = true;
            }
        }
        else if (automatic)
        {
            timer += Time.deltaTime;

            if(timer >= 1)
            {
                push = true;
            }
        }

        if (push)
        {
            print("active");
            speed = 1;
            move = true;


            if (move && !close && push)
            {
                walls[0].transform.position = Vector3.MoveTowards(walls[0].transform.position, new Vector3(transform.localPosition.x, (transform.localPosition.y + ((size.y / 2) - (walls[0].transform.localScale.y / 2))), transform.localPosition.z), speed * Time.deltaTime);
                walls[1].transform.position = Vector3.MoveTowards(walls[1].transform.position, new Vector3(transform.localPosition.x, (transform.localPosition.y - ((size.y / 2) - (walls[1].transform.localScale.y / 2))), transform.localPosition.z), speed * Time.deltaTime);

                if (walls[0].transform.position.y == (transform.localPosition.y + (walls[0].transform.localScale.y / 2)))
                {
                    speed = 0;
                    move = false;
                    close = false;
                    push = false;           
                }
            }

            if (move && close && push)
            {
                walls[0].transform.position = Vector3.MoveTowards(walls[0].transform.position, new Vector3(transform.localPosition.x, (transform.localPosition.y + (walls[0].transform.localScale.y / 2)), transform.localPosition.z), speed * Time.deltaTime);
                walls[1].transform.position = Vector3.MoveTowards(walls[1].transform.position, new Vector3(transform.localPosition.x, (transform.localPosition.y - (walls[0].transform.localScale.y / 2)), transform.localPosition.z), speed * Time.deltaTime);

                if (walls[0].transform.position.y == (transform.localPosition.y + ((size.y / 2) - (walls[0].transform.localScale.y / 2))))
                {
                    speed = 0;
                    move = false;
                    close = true;
                    push = false;
                }
            }
        }
        if (!move)
        {
            speed = 0;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        //Gizmos.DrawWireCube(transform.position, size);    
        Gizmos.DrawWireCube(Vector3.zero, size);
    }
}
