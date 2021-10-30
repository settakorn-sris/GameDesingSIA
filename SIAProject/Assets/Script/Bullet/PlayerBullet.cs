using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{

    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            return;
        }
        base.OnCollisionEnter(other);
        gameObject.SetActive(false);
    }
}

