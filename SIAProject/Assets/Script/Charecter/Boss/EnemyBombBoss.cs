using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBombBoss : Boss
{
    [SerializeField] int addDamageForBomb;
    [SerializeField] ParticleSystem partical;
    protected override void Awake()
    {
        base.Awake();
        Damage = addDamageForBomb;
    }
    public override void NormalState()
    {
        Speed = 8;
        base.NormalState();
    }
    public override void StateTwo()
    {
        Speed = 0;
        SpawnMinian();
        base.StateTwo();
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        partical.Play();
        base.OnCollisionEnter(collision);
    }
}
