using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingBullet : SBAgent
{
    public SeekingBullet(bool _useMouse, Transform _player = null)
    {
        useMouse = _useMouse;
        player = _player;
    }
    public Vector3 target;
    public bool useMouse;
    public Transform player;

    void Start()
    {
        maxSpeed = 7f;
        maxSteer = .5f;
    }

    void Update()
    {
        Vector3 mouse;
        if(useMouse){
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target = new Vector3(mouse.x, mouse.y);
        }
        else
            target = player.position;

        velocity += SteeringBehaviours.Seek(this, target, .5f);
        transform.position += velocity * Time.deltaTime;

        if((target-transform.position).sqrMagnitude < .25f)
            Destroy(gameObject);
    }

    void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, .3f);
    }
}
