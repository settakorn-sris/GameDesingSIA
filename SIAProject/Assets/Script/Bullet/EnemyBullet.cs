using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    private EnemyBulletPooling enemyBulletPool;

    private void Awake()
    {
        enemyBulletPool = EnemyBulletPooling.Instance;
    }
    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            enemyBulletPool.GetToPool(this);
            return;
        }
        base.OnCollisionEnter(other);
        print("Enter");
        enemyBulletPool.GetToPool(this);
    }
}
