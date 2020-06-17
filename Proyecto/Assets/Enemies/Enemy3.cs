using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public float patrolRange = 1f;
    public GameObject bullet;
    private bool performAction = false;
    public string state = "patrullar";

    public Transform player;
    private bool isShooting = false;
    private float bulletSpeed = 20f;

    //Falta mejorar
    private float timer = 0f;
    private float tempTimer = 0f;
    private int currentAction;

    private Quaternion rotationTarget;
    private bool activeRotationPoint = false;
    private Vector3 positionTarget;
    private bool activePositionTarget = false;
    private float MovementTime = 1f;
    private Vector3 currentPosition;
    private Vector3 mouse;

    //Detección
    public float detectRange = 5f;
    public float shootRange = 3f;

    void Start()
    {

    }

    void Update()
    {
        mouse = player.position;

        switch(state)
        {
            case "patrullar" :

            if(timer >= 6f)
            {
                //Seleccionar acción - Falta mejorar
                currentAction = Random.Range(0,2);
                //0 --> Solo rotar
                //1 --> Rotar y moverse

                activeRotationPoint = false;
                activePositionTarget = false;
                tempTimer = 0f;
                timer = 0f;
                performAction = true;
            }

            if(currentAction == 0 && performAction)
            {
                if(!activeRotationPoint)
                {
                    rotationTarget = Quaternion.Euler(0, Random.Range(0,360), 0);
                    activeRotationPoint = true;
                }

                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationTarget, 60f * Time.deltaTime);

                if(transform.rotation == rotationTarget || timer >= 5.8f) performAction = false;
            }

            else if(currentAction == 1 && performAction)
            {
                if(!activeRotationPoint && !activePositionTarget)
                {
                    positionTarget = CalcNewPos();
                    rotationTarget = Quaternion.Euler(0, CalcAngle(transform.position, positionTarget), 0);
                    currentPosition = transform.position;
                    activeRotationPoint = true;
                    activePositionTarget = true;
                }

                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationTarget, 120f * Time.deltaTime);
                
                if(transform.rotation == rotationTarget)
                {
                    tempTimer += Time.deltaTime;
                    float percentage = tempTimer/MovementTime;
                    transform.position = Vector3.Lerp(currentPosition, positionTarget, percentage);

                    if(tempTimer > MovementTime) performAction = false;
                }
            }

            if(Vector3.Distance(transform.position, mouse) <= detectRange)
            {
                if(CalcAngle(transform.position, mouse) - transform.eulerAngles.y< 15f || CalcAngle(transform.position, mouse) - transform.eulerAngles.y > 345f)
                    state = "atacar"; 
            }

            timer += Time.deltaTime; 
            break;

            case "atacar" : 

            if(Vector3.Distance(transform.position, mouse) > shootRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, mouse, 3f * Time.deltaTime);
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,CalcAngle(transform.position, mouse),0), 150f * Time.deltaTime);
            transform.LookAt(player);
            if(!isShooting)
            {
                StartCoroutine(Shoot(1, 0.25f, 1f));
            }
            break;
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
            Bullet tempBullet = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>();
            tempBullet.speedX = temp.x;
            tempBullet.speedZ= temp.y;
            //Intervalo
            if(i != shots - 1)
                yield return new WaitForSeconds(interval);
        }

        StartCoroutine(Reload(reloadTime));
    }

        Vector2 CalcSpeed()
    {
        float angle = Mathf.Atan2(mouse.z - transform.position.z, mouse.x - transform.position.x) * Mathf.Rad2Deg;
        //Debug.Log((angle<0?360+angle:angle));
        float speedX = bulletSpeed * Mathf.Cos(angle * Mathf.Deg2Rad);
        float speedZ = bulletSpeed * Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector2(speedX, speedZ);
    }

    Vector3 CalcNewPos()
    {
        //float angle = CalcAngle(transform.parent.position, transform.position);
        //float posX = Random.Range(transform.parent.position.x - patrolRange * Mathf.Cos(angle * Mathf.Deg2Rad),transform.parent.position.x + patrolRange * Mathf.Cos(angle * Mathf.Deg2Rad));
        float posX = transform.parent.position.x + patrolRange * Random.Range(-1f, 1f);
        //float posZ = Random.Range(transform.parent.position.z - patrolRange * Mathf.Sin(angle * Mathf.Deg2Rad),transform.parent.position.z + patrolRange * Mathf.Sin(angle * Mathf.Deg2Rad));
        float posZ = transform.parent.position.z + patrolRange * Random.Range(-1f, 1f);
        return new Vector3(posX, transform.position.y, posZ);
    }

    float CalcAngle(Vector3 center, Vector3 target)
    {
        float angle = Mathf.Atan2(target.x - center.x, target.z - center.z) * Mathf.Rad2Deg;
        if(angle < 0) angle = 360f + angle;
        return angle;
    }

    void OnDrawGizmos()
    {
        // float angle = 30.0f;
        // float rayRange = detectRange;
        // float halfFOV = angle / 2.0f;
        // float coneDirection = transform.rotation.y;

        // Quaternion upRayRotation = Quaternion.AngleAxis(-halfFOV + coneDirection, Vector3.forward);
        // Quaternion downRayRotation = Quaternion.AngleAxis(halfFOV + coneDirection, Vector3.forward);

        // Vector3 upRayDirection = upRayRotation * transform.right * rayRange;
        // Vector3 downRayDirection = downRayRotation * transform.right * rayRange;

        // Gizmos.DrawRay(transform.position, upRayDirection);
        // Gizmos.DrawRay(transform.position, downRayDirection);
        // Gizmos.DrawLine(transform.position + downRayDirection, transform.position + upRayDirection);

        Gizmos.color = Color.black;
        if(state == "patrullar")
            Gizmos.DrawWireSphere(transform.position, detectRange);
        else
            Gizmos.DrawWireSphere(transform.position, shootRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.parent.position, patrolRange);
    }
}
