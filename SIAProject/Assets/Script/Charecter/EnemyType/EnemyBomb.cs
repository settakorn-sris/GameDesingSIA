using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : EnemyCharecter
{
    
    public override void IsDie()
    {
        //For implement Bomb
        Hp = 0;
        base.IsDie();
    }
}
