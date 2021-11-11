using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDamageSkill : Skill
{
    [SerializeField]private int damgeForAdd = 10;
    private int oldDamage;
  

    public override void AboutSkill(PlayerCharecter player)
    {
        base.AboutSkill(player);
        StartCoroutine(Plus(player));
        Debug.Log(player.GM.BulletDamage);
    }
    private IEnumerator Plus(PlayerCharecter player)
    {
        oldDamage = player.GM.BulletDamage;
        player.GM.BulletDamage += damgeForAdd;

        yield return new WaitForSeconds(timeOfSkill);
        EndSkill(player);
    }
    protected override void EndSkill(PlayerCharecter player)
    {
        base.EndSkill(player);
        player.GM.BulletDamage = oldDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
