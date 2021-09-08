using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Charecter : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    public int Hp;

    public float Speed;
    private void Start()
    {
        healthBar.SetMaxHealth(Hp);
    }
    private void Update()
    {
        healthBar.SetHealth(Hp);
    }
    public void Init(int hp, float speed)
    {
        this.Hp = hp;
        this.Speed = speed;
    }
    public abstract  void Attack();
    
    
}
