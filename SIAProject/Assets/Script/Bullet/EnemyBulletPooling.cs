using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPooling : Singleton<EnemyBulletPooling>
{
    [SerializeField] Bullet enemyBullet;
    private int bulletAmount;
    private Queue<Bullet> enemyBulletQueue = new Queue<Bullet>();
    private GameManager gM;


    private void Awake()
    {
        gM = GameManager.Instance;
        enemyBullet = gM.EnemyBullet;
    }

    private void SpawnBullet()
    {
        for(var i =0; i>=bulletAmount;i--)
        {
            var bullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
            enemyBulletQueue.Enqueue(bullet);
            bullet.gameObject.SetActive(false);
        }
    }

    public void GetBullet(GameObject firePosition)
    {
        if(enemyBulletQueue.Count>0)
        {
            var enemyBullet = enemyBulletQueue.Dequeue();

            enemyBullet.transform.position = firePosition.transform.position;               //get bullet Speed From Gm 
            enemyBullet.Rb.velocity = firePosition.transform.forward * enemyBullet.GetSpeed(gM.BulletSpeed);
        }
        else
        {
            var bullet = Instantiate(enemyBullet,firePosition.transform.position, Quaternion.identity);
        }

    }

    public void GetToPool(Bullet bullet)
    {
        enemyBulletQueue.Enqueue(bullet);
        bullet.gameObject.SetActive(false);
    }
  

}
