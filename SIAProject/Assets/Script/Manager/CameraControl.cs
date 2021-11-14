using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform player;
    private Vector3 CameraOffset;

    private float Smooth = 1f;
    private float shakeTime = 0.15f;

    public AnimationCurve curve;
    public bool isShake = false;
    public bool SetShake
    {
        get
        {
            return isShake;
        }
        set
        {
            isShake = value;
        }
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        CameraOffset = transform.position - player.transform.position;
        transform.position = CameraOffset;
    }
    private void Update()
    {
        Vector3 upPosition = player.transform.position + CameraOffset;
        transform.position = Vector3.Slerp(transform.position, upPosition, Smooth);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isShake)
        {
            isShake = false;
            StartCoroutine(CameraShake());
        }
    }

    public IEnumerator CameraShake()
    {
        Vector3 oldPosition = transform.position;
        float timeCal = 0;

        while (timeCal < shakeTime)
        {
            timeCal += Time.deltaTime;
            float stangth = curve.Evaluate(timeCal / shakeTime);
            transform.position = oldPosition + Random.insideUnitSphere;
            yield return null;
        }

        transform.position = oldPosition;


    }


    //public void CameraShake()
    //{
    //    Vector3 shake = transform.rotation.eulerAngles + new Vector3(0, 0, 3);


    //    transform.rotation = Quaternion.Euler(shake);


    //}
}
