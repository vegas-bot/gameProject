using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float radius = 8f;
    public bool isShooting = false;
    private Vector3 target;
    public float bulletSpeed = 10f;

    public bool useMouse = true;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Cambiar por player
        if(useMouse){
            Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target = new Vector3(temp.x, temp.y, 0);
        }
        else{
            target = player.position;
        }
        //
        if(Vector3.Distance(transform.position, target) <= radius)
        {
            if(!isShooting)
            {
                StartCoroutine(Shoot(3, 0.25f, 1f));
            }
        }
    }

    IEnumerator Reload(float time)
    {
        yield return new WaitForSeconds(time);
        isShooting = false;

    }

    IEnumerator Shoot(int shots, float interval, float reloadTime)
    {
        isShooting = true;
        Vector2 temp = CalcSpeed();
        for(int i = 0; i < shots; i++)
        {
            //Disparar
            Bullet tempBullet = new GameObject("Bullet"+i).AddComponent<Bullet>();
            tempBullet.speedX = temp.x;
            tempBullet.speedZ= temp.y;
            //Intervalo
            yield return new WaitForSeconds(interval);
        }

        StartCoroutine(Reload(reloadTime));
    }

    Vector2 CalcSpeed()
    {
        float angle = Mathf.Atan2(target.z - transform.position.z, target.x - transform.position.x) * Mathf.Rad2Deg;
        //Debug.Log((angle<0?360+angle:angle));
        float speedX = bulletSpeed * Mathf.Cos(angle * Mathf.Deg2Rad);
        float speedZ = bulletSpeed * Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector2(speedX, speedZ);
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
