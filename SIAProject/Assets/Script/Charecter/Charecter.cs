using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Charecter : MonoBehaviour,ITakeDamage
{
    [SerializeField] private HealthBar healthBar;
    public int Hp;

    public float Speed;
    public event Action OnDie;
    private void Start()
    {
        healthBar.SetMaxHealth(Hp);
        OnDie += IsDie;
    }
    private void Update()
    {
        healthBar.SetHealth(Hp);
    }
    protected void Init(int hp, float speed)
    {
        this.Hp = hp;
        this.Speed = speed;
    }

    public void TakeDamage(int damage)
    {
        Hp -= damage;
        if (Hp > 0) return;
        OnDie();
    }
  
    public abstract void IsDie();
    
    
    
}
