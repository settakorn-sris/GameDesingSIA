using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharecter : Charecter
{
    [SerializeField] private PlayerBullet bulletType;
    [SerializeField] private PoolingOBJ bullet;


    //public void GetBullet(PoolingOBJ bullet)
    //{
    //    this.bullet = bullet;
    //}
    private void Awake()
    {
        bullet.GetBulletType(bulletType);
    }
    public override void Attack()
    {
        bullet.GetPool();
    }
   
}
