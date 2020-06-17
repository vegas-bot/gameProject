using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speedX;
    public float speedZ;

    public Bullet(float _speedX, float _speedZ)
    {
        speedX = _speedX;
        speedZ = _speedZ;
    }

    void Start()
    {
        StartCoroutine(TimeDestroy());
    }

    IEnumerator TimeDestroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    void Update()
    {
        transform.position += new Vector3(speedX, 0, speedZ) * Time.deltaTime;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
