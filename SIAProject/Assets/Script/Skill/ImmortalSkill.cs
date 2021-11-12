using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalSkill : Skill
{
    public override void AboutSkill(PlayerCharecter player)
    {
        base.AboutSkill(player);
        StartCoroutine(Immortal(player));
    }

    private IEnumerator Immortal(PlayerCharecter player)
    {
        player.CanGetDamage = false;
        yield return new WaitForSeconds(timeOfSkill);
        EndSkill(player);
    }
    protected override void EndSkill(PlayerCharecter player)
    {
        base.EndSkill(player);
        player.CanGetDamage = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
