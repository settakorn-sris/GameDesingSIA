using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunt : Skill
{
    //public int StuntTime;
    public override void AboutSkill(PlayerCharecter player)
    {
        base.AboutSkill(player);
        StartCoroutine(ControlStunt(player));

    }

    protected override void EndSkill(PlayerCharecter player)
    {
        player.GM.CheckSkillCollision.gameObject.SetActive(false);
    }

    private IEnumerator ControlStunt(PlayerCharecter player)
    {
        player.GM.CheckSkillCollision.transform.position = player.transform.position;
        player.GM.CheckSkillCollision.SetActive(true);
        yield return new WaitForSeconds(timeOfSkill);
        EndSkill(player);
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
