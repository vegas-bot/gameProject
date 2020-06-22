using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class enemy2v1 : MonoBehaviour
{

    public GameObject Player;
    public Vector3 aim;
    public GameObject bullet;
    private float timer;
    private float angulo;
    private float X;
    private float Z;
    [HideInInspector]
    public float posX;
    [HideInInspector]
    public float posZ;

    public Slider lifebar;  
    int life;

    void Start()
    {
        aim  = transform.position;
        life = 125;
    }

   
    void Update()
    {
        lifebar.value = life;
        //Accion
        if (Vector3.Distance(transform.position,Player.transform.position) < 80f)
        {
            apuntar();
            timer += Time.deltaTime;
            if(timer >= 1)
            {
                GameObject bala = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), Quaternion.identity);
                bala.GetComponent<bullet2v1>().speedX = posX;
                bala.GetComponent<bullet2v1>().speedZ = posZ;

                timer = 0;
            }
        }
        else
        {
            timer = 0; 
        }

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    void apuntar()
    {
        Z = Player.transform.position.z - transform.position.z;
        X = Player.transform.position.x - transform.position.x;

        angulo = Mathf.Atan2(Z,X) * Mathf.Rad2Deg;

        posX = 40 * Mathf.Cos(angulo * Mathf.Deg2Rad);
        posZ = 40 * Mathf.Sin(angulo * Mathf.Deg2Rad);

        aim = new Vector3(transform.position.x + posX,Player.transform.position.y , transform.position.z + posZ);
        transform.rotation = Quaternion.Euler(-90, transform.rotation.y - angulo, 0);
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
