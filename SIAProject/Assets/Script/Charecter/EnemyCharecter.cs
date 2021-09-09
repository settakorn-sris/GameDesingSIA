using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharecter : Charecter
{
    [SerializeField]private int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<ITakeDamage>();
        player?.TakeDamage(damage);
        print("atk");
    }

    public override void IsDie()
    {
        Destroy(gameObject);
        ScoreManager.Instance.AddScore(1);
    }
}
