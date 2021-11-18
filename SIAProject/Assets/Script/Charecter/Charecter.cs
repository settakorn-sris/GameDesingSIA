using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Charecter : MonoBehaviour,ITakeDamage
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private TakeDamagePopUp damagePopUp;

    public int Hp;

    public float Speed;
    public event Action OnDie;
    private void Start()
    {
        healthBar.SetMaxHealth(Hp);
        OnDie += IsDie;
    }
    protected virtual void Update()
    {
        healthBar.SetHealth(Hp);
    }
    protected void Init(int hp, float speed)
    {
        this.Hp = hp;
        this.Speed = speed;
    }

    public virtual void TakeDamage(int damage)
    {
        Hp -= damage;
        var damagePop = Instantiate(damagePopUp, transform.position, Quaternion.identity);
        damagePop.PopupActive(damage);
        if (Hp > 0) return;
        OnDie();
    }

    
    public abstract void IsDie();
    
    
    
}
