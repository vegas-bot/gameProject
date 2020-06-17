using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6 : SBAgent
{
    public float detectRange = 3f;
    private float shootRange = 10f;
    private string state = "detect";
    private float reloadTime = 2f;
    private bool canShoot = false;

    public bool useMouse = true;
    public Transform player;

    public bool initDestroy = false;
    private int bulletCount = 0;

    void Start()
    {
    }


    void Update()
    {
        Vector3 mouse;
        if(useMouse){
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse = new Vector3(mouse.x, mouse.y);
        }else
            mouse = player.position;

        switch(state)
        {
            case "detect" :
                if((mouse - transform.position).sqrMagnitude < detectRange * detectRange || (GameObject.Find("Enemy7").transform.position - transform.position).sqrMagnitude < detectRange * detectRange)
                {
                    canShoot = true;
                    state = "shoot";
                }
                break;
            case "shoot" :
                if((mouse - transform.position).sqrMagnitude > shootRange * shootRange)
                    velocity += SteeringBehaviours.Seek(this, mouse, shootRange);
                else
                    velocity += SteeringBehaviours.Flee(this, mouse, shootRange);

                transform.position += velocity * Time.deltaTime;
                if(canShoot)
                    Shoot();
                break;
        }
        
    }

    void Shoot()
    {
        GameObject bullet = new GameObject("bullet" + bulletCount.ToString());
        bullet.transform.position = transform.position;
        bullet.transform.SetParent(gameObject.transform);
        bullet.AddComponent<SeekingBullet>().useMouse = false;
        bullet.AddComponent<SeekingBullet>().player = player;
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
}
