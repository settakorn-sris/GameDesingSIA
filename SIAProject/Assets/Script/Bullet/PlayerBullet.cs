using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    private void Awake()
    {

        rb.velocity = new Vector3(1, 0, 0) * speed;

    }
    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject.GetComponent<ITakeDamage>();
        enemy?.TakeDamage(damage);
        gameObject.SetActive(false);
    }
}
