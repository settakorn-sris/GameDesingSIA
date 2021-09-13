using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharecter : Charecter
{
    [SerializeField]private int damage = 10;
    [SerializeField]private ParticleSystem dieParticle;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<ITakeDamage>();
        player?.TakeDamage(damage);
        print("atk");
    }

    public override void IsDie()
    {
        Instantiate(dieParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
        ScoreManager.Instance.AddScore(1);
    }
}
