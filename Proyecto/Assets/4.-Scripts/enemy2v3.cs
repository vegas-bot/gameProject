using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy2v3 : MonoBehaviour
{
    public GameObject Player;
    public Vector3 aim;
    private float timer;
    private float angulo;
    private float X;
    private float Y;
    [HideInInspector]
    public float posX;
    [HideInInspector]
    public float posY;
    public Transform hittarget;

    public Slider lifebar;
    int life;

    void Start()
    {
        aim = transform.position;
        life = 200;
    }


    void Update()
    {
        lifebar.value = life;

        //Accion
        if (Vector3.Distance(transform.position, Player.transform.position) < 80f)
        {
            apuntar();
            print("shoot");
            hittarget.position = Player.transform.position;
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                print("shoot");
                timer = 0;
            }
        }
        else
        {
            timer = 0;
            hittarget.position = Vector3.zero;
        }

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    void apuntar()
    {
        Y = Player.transform.position.z - transform.position.z;
        X = Player.transform.position.x - transform.position.x;

        angulo = Mathf.Atan2(Y, X) * Mathf.Rad2Deg;

        posX = 20 * Mathf.Cos(angulo * Mathf.Deg2Rad);
        posY = 20 * Mathf.Sin(angulo * Mathf.Deg2Rad);

        aim = new Vector3(transform.position.x + posX,Player.transform.position.y ,transform.position.z + posY );
        transform.rotation = Quaternion.Euler(-90, transform.rotation.y - angulo,0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 80f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(aim, 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "bulletplayer")
        {

            life -= 5;
            Destroy(other.gameObject);
        }
    }
}

