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
        transform.LookAt(transform.position + cameraToLookAt.transform.rotation * new Vector3(0, 0, -1), cameraToLookAt.transform.rotation * new Vector3(0, 1, 0));
    }
}
