using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    private float patrolRange = 1f;
    private bool performAction = false;
    public string state = "patrullar";

    public bool useMouse = true;
    public Transform player;

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
    private float detectRange = 5f;
    private float shootRange = 3f;

    void Start()
    {

    }

    void Update()
    {
        if(useMouse)
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            else
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
                    rotationTarget = Quaternion.Euler(0, 0, Random.Range(0,360));
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
                    rotationTarget = Quaternion.Euler(0, 0, CalcAngle(transform.position, positionTarget));
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
                if(CalcAngle(transform.position, mouse) - transform.eulerAngles.z< 15f || CalcAngle(transform.position, mouse) - transform.eulerAngles.z > 345f)
                    for(int i = 0; i < transform.parent.parent.childCount; i++)
                    {
                        transform.parent.parent.GetChild(i).GetChild(0).GetComponent<Enemy3>().state = "atacar";
                    }
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
            break;
        }
    }

    Vector2 CalcNewPos()
    {
        float angle = CalcAngle(transform.parent.position, transform.position);
        float posX = Random.Range(transform.parent.position.x - patrolRange * Mathf.Cos(angle * Mathf.Deg2Rad),transform.parent.position.x + patrolRange * Mathf.Cos(angle * Mathf.Deg2Rad));
        float posY = Random.Range(transform.parent.position.y - patrolRange * Mathf.Sin(angle * Mathf.Deg2Rad),transform.parent.position.y + patrolRange * Mathf.Sin(angle * Mathf.Deg2Rad));
        return new Vector2(posX, posY);
    }

    float CalcAngle(Vector3 center, Vector3 target)
    {
        float angle = Mathf.Atan2(target.y - center.y, target.x - center.x) * Mathf.Rad2Deg;
        if(angle < 0) angle = 360f + angle;
        return angle;
    }

    void OnDrawGizmos()
    {
        float angle = 30.0f;
        float rayRange = detectRange;
        float halfFOV = angle / 2.0f;
        float coneDirection = transform.rotation.z;

        Quaternion upRayRotation = Quaternion.AngleAxis(-halfFOV + coneDirection, Vector3.forward);
        Quaternion downRayRotation = Quaternion.AngleAxis(halfFOV + coneDirection, Vector3.forward);

        Vector3 upRayDirection = upRayRotation * transform.right * rayRange;
        Vector3 downRayDirection = downRayRotation * transform.right * rayRange;

        Gizmos.DrawRay(transform.position, upRayDirection);
        Gizmos.DrawRay(transform.position, downRayDirection);
        Gizmos.DrawLine(transform.position + downRayDirection, transform.position + upRayDirection);

        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.parent.position, patrolRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, .3f);
    }
}
