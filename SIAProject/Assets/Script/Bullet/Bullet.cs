using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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

    protected virtual void OnCollisionEnter(Collision other)
    {
        var enemy = other.gameObject.GetComponent<ITakeDamage>();
        enemy?.TakeDamage(damage);
        
    }
}
