using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7 : SBAgent
{
    private float detectionRadius = 25f;
    private string state = "patrol";
    public Transform target;
    public Transform enemy;
    private Vector3 initPos;

    public Transform player;

    void Start()
    {
        initPos = transform.position;
        maxSpeed = 10f;
    }

    void Update()
    {
        Vector3 mouse;
        mouse = player.position;

        switch(state)
        {
            case "patrol" : 
                Renderer ren1 = GetComponent<Renderer>();
                ren1.sharedMaterial.color = Color.green;
                velocity = Vector3.zero;
                if((mouse - transform.position).sqrMagnitude < detectionRadius * detectionRadius)
                {
                    target = enemy;
                    new GameObject("initPos").transform.position = initPos;
                    maxSpeed *= 2;
                    state = "detect";
                }
            break;
            case "detect" :
                Renderer ren = GetComponent<Renderer>();
                ren.sharedMaterial.color = Color.red;
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
