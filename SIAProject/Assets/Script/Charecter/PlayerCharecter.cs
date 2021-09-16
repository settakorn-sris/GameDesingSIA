using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharecter : Charecter
{
    [SerializeField] private PlayerBullet bulletType;
    [SerializeField] private PoolingOBJ bullet;

    private GameManager GM;
    //public void GetBullet(PoolingOBJ bullet)
    //{
    //    this.bullet = bullet;
    //}
    public void Init(int hp, float speed)
    {
        base.Init(hp, speed);
    }
    private void Awake()
    {
        GM = GameManager.Instance;
        bullet.GetBulletType(bulletType);
    }
    public void Attack()
    {
        bullet.GetPool();
        //Instantiate(bulletType, bullet.transform.position, Quaternion.identity);
    }
    public void ChangeBullet()
    {
        //For Change Bullet in pool
    }
    public override void IsDie()
    {
        gameObject.SetActive(false);
        
    }

}
