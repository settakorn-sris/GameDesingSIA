using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeBoss : Boss
{
    private float fireRate;
    private float countFireRate = 0;

    [SerializeField] GameObject firePosition;
    private EnemyBulletPooling enemyBulletPooling;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float rotateSpeed;
 
    protected override void Awake()
    {
        base.Awake();
        enemyBulletPooling = EnemyBulletPooling.Instance;

        fireRate = gM.EnemyFireRate;
    }
    protected override void Update()
    {
        base.Update();

        if (countFireRate >= fireRate)
        {
            UseBullet();
            countFireRate = 0;
        }
        countFireRate += Time.deltaTime;
    }
    private void UseBullet()
    {
        enemyBulletPooling.GetBullet(firePosition, gM.getBossBulletDamage);
    }

    public override void NormalState()
    {
        // transform.rotation = Quaternion.Euler(new Vector3(0, 5, 0));
        //rb.AddRelativeTorque(0, 50, 0);
        // transform.Rotate(rotation*rotateSpeed * Time.deltaTime);
        Hp = gM.GetBossHp;
        base.NormalState();
    }
    public override void StateTwo()
    {
        // transform.rotation = Quaternion.Euler(Vector3.zero);
       // transform.Rotate(Vector3.zero);
        base.StateTwo();
    }
}
