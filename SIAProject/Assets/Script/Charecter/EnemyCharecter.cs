using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharecter : Charecter
{
    private int damage = 10;
    [SerializeField]private ParticleSystem dieParticle;
    private GameManager gM;
    private void Awake()
    {
        gM = GameManager.Instance;
    }

    public void Init(int hp, float speed,int damage)
    {
        base.Init(hp, speed);
        this.damage = damage;

    }
    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<ITakeDamage>();
        player?.TakeDamage(damage);
    }
    public override void IsDie()
    {
        Instantiate(dieParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
        ScoreManager.Instance.AddScore(1);
    }
}
