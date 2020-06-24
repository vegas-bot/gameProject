using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6 : SBAgent
{
    public float detectRange = 3f;
    public float shootRange = 30f;
    private string state = "detect";
    private float reloadTime = 2f;
    private bool canShoot = false;

    public bool useMouse = true;
    public Transform player;
    public GameObject bullet;
    public Transform bulletStorage;
    public Transform enemyAlly;

    public bool initDestroy = false;
    private int bulletCount = 0;


    void Start()
    {
    }


    void Update()
    {

        Vector3 mouse;
        mouse = player.position;

        switch(state)
        {
            case "detect" :
                if((mouse - transform.position).sqrMagnitude < detectRange * detectRange || (enemyAlly.position - transform.position).sqrMagnitude < detectRange * detectRange)
                {
                    canShoot = true;
                    state = "shoot";
                }
                break;
            case "shoot" :

                if((mouse - transform.position).sqrMagnitude > (shootRange + 1f) * (shootRange + 1f))
                {
                    maxSteer = 1f;
                    velocity += SteeringBehaviours.Seek(this, mouse, shootRange);
                }
                else if((mouse - transform.position).sqrMagnitude < (shootRange - 1f) * (shootRange - 1f))
                {
                    maxSteer  = 1f;
                    velocity += SteeringBehaviours.Flee(this, mouse, shootRange);
                }
                else
                    maxSteer = 0;

                transform.position += velocity * Time.deltaTime;
                if(canShoot)
                    Shoot();
                break;
        }
    }

    void Shoot()
    {
        GameObject bala = Instantiate(bullet, transform.position, Quaternion.identity, bulletStorage);
        bala.name = "bullet" + bulletCount.ToString("00");
        bala.GetComponent<SeekingBullet>().player = player;
        bulletCount++;
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        canShoot = false;
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
    }

    void OnDrawGizmos()
    {
        if(state == "detect")
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, detectRange);
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
}
