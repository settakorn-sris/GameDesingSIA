using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{

    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player") return;
        base.OnCollisionEnter(other);
    }
}

