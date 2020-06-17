using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamvasFit : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        transform.position = player.position + Vector3.up * 0.5f;
    }
}
