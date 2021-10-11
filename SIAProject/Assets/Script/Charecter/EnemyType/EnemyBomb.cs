using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : EnemyCharecter
{
    

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        IsDie();
    }
    public override void IsDie()
    {
        //For implement Bomb
        base.IsDie();
    }
}
