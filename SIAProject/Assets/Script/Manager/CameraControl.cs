using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform player;
    private Vector3 CameraOffset;

    private float Smooth = 0.05f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        CameraOffset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 upPosition = player.transform.position + CameraOffset;
        transform.position = Vector3.Slerp(transform.position, upPosition, Smooth);
        
    }
}
