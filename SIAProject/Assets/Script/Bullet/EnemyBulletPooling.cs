using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPooling : Singleton<EnemyBulletPooling>
{
    private Bullet enemyBullet;
   [SerializeField] private int bulletAmount;
    private Queue<Bullet> enemyBulletQueue = new Queue<Bullet>();
    //private Queue<Bullet> playerBullet = new Queue<Bullet>();
    private GameManager gM;


    private void Awake()
    {
        gM = GameManager.Instance;
        enemyBullet = gM.EnemyBullet;
        SpawnBullet();
    }

    private void SpawnBullet()
    {
        for(var i =0; i<bulletAmount;i++)
        {
            var bullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
            enemyBulletQueue.Enqueue(bullet);
            bullet.gameObject.SetActive(false);
        }
    }

    public void GetBullet(GameObject firePosition,int bulletDamage)
    {
        if (enemyBulletQueue.Count > 0)
        {

            var enemyBullet = enemyBulletQueue.Dequeue();
            enemyBullet.gameObject.SetActive(true);
            enemyBullet.GetDamage(bulletDamage);
            enemyBullet.transform.position = firePosition.transform.position;               //get bullet Speed From Gm 
            enemyBullet.Rb.velocity = firePosition.transform.forward * enemyBullet.GetSpeed(gM.BulletSpeed);
        }
        else
        {
            var bullet = Instantiate(enemyBullet, firePosition.transform.position, Quaternion.identity);
        }

    }

    public void GetToPool(Bullet bullet)
    {
        print("Get TO Pool");
        enemyBulletQueue.Enqueue(bullet);
        
        bullet.gameObject.SetActive(false);
    }
  

}
