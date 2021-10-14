using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyCharecter
{
    [SerializeField] private EnemyCharecter minian;
    
    private int count = 0;
    private int minianAmount;
    private int hpForHeal;
   
    

    protected override void Awake()
    {
        base.Awake();
        hpForHeal = gM.HpForBossHealing;
        minianAmount = gM.minianAmount;
       
        //minian = gM.MinianOfBoss;
        minian.Init(20,3,10);
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        count++;
        if(count >= 2 && collision.gameObject.tag==("Player"))
        {
            Hp += hpForHeal;
            count = 0;
        }
    }
    public override void TakeDamage(int damage)
    {
        if (Hp <= (gM.GetBossHp/2))
        {
            Debug.Log("Spawn");
            SpawnMinian();

        }
        
        base.TakeDamage(damage);

    }
    public override void IsDie()
    {
        base.IsDie();
    }
    private void SpawnMinian()
    {

        while (minianAmount > 0)
        {

            var xPosition = Random.Range(-12, 13);
            var zPosition = Random.Range(-11, 13);

            Instantiate(minian, new Vector3(xPosition, 0, zPosition), Quaternion.identity);
            minianAmount--;
        }
    }
}
