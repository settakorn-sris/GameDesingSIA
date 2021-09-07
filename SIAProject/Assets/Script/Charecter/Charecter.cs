using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Charecter : MonoBehaviour
{
    public int Hp { get { return Hp; } set { if (value <= 10) Hp = 100; } }

    public float Speed { get { return Speed; } set { if (value <= 10) Speed = 10; } }
    

    public void Init(int hp, float speed)
    {
        this.Hp = hp;
        this.Speed = speed;
    }
    public abstract  void Attack();
    
    
}
