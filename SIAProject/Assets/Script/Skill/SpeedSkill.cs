using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSkill : Skill
{
   [SerializeField] private float addSpeed = 45;
    private float oldSpeed ;
    public override void AboutSkill(PlayerCharecter player)
    {
        base.AboutSkill(player);
        StartCoroutine(Speed(player));
        
    }

    private IEnumerator Speed(PlayerCharecter player)
    {
        oldSpeed = player.Speed;
        player.Speed = addSpeed;
        print("speed");


        yield return new WaitForSeconds(timeOfSkill);
        EndSkill(player);
    }

    protected override void EndSkill(PlayerCharecter player)
    {
        base.EndSkill(player);
        player.Speed = oldSpeed;
    }

  

    private void Update()
    {
        
    }
}
