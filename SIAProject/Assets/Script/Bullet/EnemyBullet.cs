using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    
    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy"|| other.gameObject.tag == "Boss") return;
        base.OnCollisionEnter(other);
    }
}
