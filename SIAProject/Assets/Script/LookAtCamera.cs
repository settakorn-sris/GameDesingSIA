using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Camera cameraToLookAt;
    private void Start()
    {
        cameraToLookAt = Camera.main;
    }
    void Update()
    {
        //Vector3 v = cameraToLookAt.transform.position - transform.position;
        //v.x = v.z = 0.0f;
        //transform.LookAt(cameraToLookAt.transform.position - v);
        //transform.Rotate(0, 180, 0);

        transform.LookAt(transform.position + cameraToLookAt.transform.rotation * new Vector3(0, 0, -1), cameraToLookAt.transform.rotation * new Vector3(0, 1, 0));
    }
}
