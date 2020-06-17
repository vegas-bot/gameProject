using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7 : SBAgent
{
    private float detectionRadius = 4f;
    private string state = "patrol";
    public Transform target;
    private Vector3 initPos;

    public bool useMouse = true;
    public Transform player;

    void Start()
    {
        initPos = transform.position;
        maxSpeed = 2f;
    }

    void Update()
    {
        Vector3 mouse;
        if(useMouse){
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse = new Vector3(mouse.x, mouse.y);
        }
        else
            mouse = player.position;

        switch(state)
        {
            case "patrol" : 
                velocity = Vector3.zero;
                if((mouse - transform.position).sqrMagnitude < detectionRadius * detectionRadius)
                {
                    target = GameObject.Find("Enemy6").transform.GetChild(0);
                    new GameObject("initPos").transform.position = initPos;
                    maxSpeed *= 2;
                    state = "detect";
                }
            break;
            case "detect" :
                float range = target.GetComponent<Enemy6>().detectRange;
                velocity += SteeringBehaviours.Seek(this, target.position, range);
                if((target.position - transform.position).sqrMagnitude < range * range)
                {
                    target = GameObject.Find("initPos").transform;
                    maxSpeed *= .5f;
                    state = "reset";
                }
            break;
            case "reset" : 
                velocity += SteeringBehaviours.Seek(this, target.position, .5f);
                if((target.position - transform.position).sqrMagnitude < .25f)
                {
                    Destroy(target.gameObject);
                    target = null;
                    state = "patrol";
                }
            break;
        }

        transform.position += velocity * Time.deltaTime;
    }

    void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
