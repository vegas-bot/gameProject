using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy2v2 : MonoBehaviour
{
    public GameObject Player;
    public Vector3 aim;
    public GameObject bullet;
    private float timer;
    private float angulo;
    private float X;
    private float Y;
    [HideInInspector]
    public float posX;
    [HideInInspector]
    public float posY;
    public Slider lifebar;
    int life;

    void Start()
    {
        aim = transform.position;
        life = 250;
    }


    void Update()
    {
        lifebar.value = life;
        //Accion
        if (Vector3.Distance(transform.position, Player.transform.position) < 20f)
        {
            apuntar();
            print("shoot");
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                GameObject bala = Instantiate(bullet, transform.position, Quaternion.Euler(posX,posY,0));
                bala.GetComponent<bullet2v2>().speedX = posX;
                bala.GetComponent<bullet2v2>().speedY = posY;
                print("shoot");
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
        Y = Player.transform.position.z - transform.position.z;
        X = Player.transform.position.x - transform.position.x;

        angulo = Mathf.Atan2(Y, X) * Mathf.Rad2Deg;

        posX = 20 * Mathf.Cos(angulo * Mathf.Deg2Rad);
        posY = 20 * Mathf.Sin(angulo * Mathf.Deg2Rad);

        aim = new Vector3(transform.position.x + posX,Player.transform.position.y , transform.position.z + posY);
        transform.rotation = Quaternion.Euler(-90,transform.rotation.y - angulo, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 20f);

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
