using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : EnemyCharecter
{
   [SerializeField] private int bombDamage;

    protected override void OnCollisionEnter(Collision collision)
    {
        Damage += gM.GetBombDamage;
        base.OnCollisionEnter(collision);
        print("BoooM2");
        if (collision.gameObject.tag == "Player") IsDie();
      
       


    }
    public override void IsDie()
    {
        //For implement Bomb
        base.IsDie();
    }
}
