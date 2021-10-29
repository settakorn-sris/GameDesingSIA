using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingOBJ : MonoBehaviour
{
     private Bullet bullet;
    [SerializeField] private Bullet[] bulletInPool;
    [SerializeField] private int poolAmount = 0;
    [SerializeField] private float fireRate = 1f;//Player => GM => Pool (If we want to manage fireRate in plyer)  f
    [SerializeField] private float timeCountForFire = 0;

    private int ControlPool = 0;
    private GameManager GM;

    private void Awake()
    {
        GM = GameManager.Instance;
        fireRate = GM.FireRate;
    }
    void Start()
    {
        bulletInPool = new Bullet[poolAmount];
        Pooling();
    }
    private void Update()
    {
       
    }
    private void Pooling()
    {
        for (var i = 0; i < poolAmount; i++)
        {
            bulletInPool[i] = Instantiate(bullet, transform.position, Quaternion.identity);
            bulletInPool[i].transform.parent = transform;
            bulletInPool[i].gameObject.SetActive(false);
        }
    }
    public void GetPool()
    {
        if (ControlPool == poolAmount)
        {
            ControlPool = 0;
        }

        if (Time.time < timeCountForFire) return;

        bulletInPool[ControlPool].transform.position = transform.position;
        //Get DM from GM;
        bulletInPool[ControlPool].GetDamage(GM.BulletDamage);
        bulletInPool[ControlPool].gameObject.SetActive(true);
        bulletInPool[ControlPool].transform.parent = transform;//For tranForm bullet
        //Get Speed from GM;
        bulletInPool[ControlPool].Rb.velocity = transform.forward * bulletInPool[ControlPool].GetSpeed(GM.BulletSpeed);
        timeCountForFire = Time.time + fireRate;
        ControlPool++;
       
    }

    public void GetBulletType(Bullet bulletType)
    {
        bullet = bulletType;
    }

}
