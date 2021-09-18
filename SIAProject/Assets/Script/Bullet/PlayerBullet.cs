using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] public Rigidbody Rb;
    [SerializeField] private int damage;
    
    private int speed;

    public int GetDamage(int damage)
    {
        return this.damage = damage;
    }
    public int GetSpeed(int bulletSpeed)
    {
        return speed = bulletSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject.GetComponent<ITakeDamage>();
        enemy?.TakeDamage(damage);
        gameObject.SetActive(false);
    }
}
