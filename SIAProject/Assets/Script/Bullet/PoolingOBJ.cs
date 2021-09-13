using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingOBJ : MonoBehaviour
{
     private PlayerBullet bullet;
    [SerializeField] private int bulletSpeed;
    [SerializeField] private PlayerBullet[] bulletInPool;
    [SerializeField] private int poolAmount = 0;
    //[SerializeField] private Vector3 gunPosition;

    private int ControlPool = 0;


    void Start()
    {
        bulletInPool = new PlayerBullet[poolAmount];
        Pooling();
    }

    public void GetPool()
    {
        if (ControlPool == poolAmount)
        {
            ControlPool = 0;
        }

        bulletInPool[ControlPool].transform.position = transform.position;
        bulletInPool[ControlPool].gameObject.SetActive(true);
        bulletInPool[ControlPool].rb.velocity = transform.forward * bulletInPool[ControlPool].GetSpeed(bulletSpeed);
        ControlPool++;


    }

    public void GetBulletType(PlayerBullet bulletType)
    {
        bullet = bulletType;
    }
    private void Pooling()
    {
        for(var i=0;i<poolAmount;i++)
        {
            bulletInPool[i] = Instantiate(bullet,transform.position, Quaternion.identity);
            bulletInPool[i].gameObject.SetActive(false);
        }
    }

}
