using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharecter : Charecter
{
    public int Damage;
    private GameManager gM;
    [SerializeField] private int scoreInFirstRound = 1;
    [SerializeField] protected ParticleSystem dieParticle;

    private void Awake()
    {
        gM = GameManager.Instance;
    }

    public void Init(int hp, float speed,int damage)
    {
        base.Init(hp, speed);
        Damage = damage;

    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var player = collision.gameObject.GetComponent<ITakeDamage>();
            player?.TakeDamage(Damage);
        }
     
    }
    public override void IsDie()
    {
        Instantiate(dieParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
        ScoreManager.Instance.AddScore(scoreInFirstRound);
    }
}
