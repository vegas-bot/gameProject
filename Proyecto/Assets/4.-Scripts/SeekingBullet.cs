using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingBullet : SBAgent
{
    public SeekingBullet(Transform _player)
    {
        player = _player;
    }
    public Vector3 target;
    public Transform player;
    public float targetTime;

    void Start()
    {
        maxSpeed = 20f;
        maxSteer = 1f;
        targetTime = 2f;
    }

    void Update()
    {
        if(targetTime > 0f){
            target = player.position;
            transform.LookAt(target);
        }
        else
            target = transform.position + transform.forward;


        velocity += SteeringBehaviours.Seek(this, target, .5f);
        transform.position += velocity * Time.deltaTime;

        if((target-transform.position).sqrMagnitude < .25f)
            Destroy(gameObject);

        targetTime -= Time.deltaTime;
        Debug.Log(targetTime);
    }

    void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, .3f);
    }
}
