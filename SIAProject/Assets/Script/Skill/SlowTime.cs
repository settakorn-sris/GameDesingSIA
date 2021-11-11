using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : Skill
{
   
    [SerializeField] private float forSlowSpeed = 0.25f;
    private float enemySpeed;
    public override void AboutSkill(PlayerCharecter player)
    {
        base.AboutSkill(player);
        StartCoroutine(Slow(player));
    }

    protected override void EndSkill(PlayerCharecter player)
    {
        base.EndSkill(player);
        player.GM.TimeSlow(enemySpeed);
    }
    
    private IEnumerator Slow(PlayerCharecter player)
    {
        player.GM.TimeSlow(forSlowSpeed);
        enemySpeed = player.GM.GetEnemySpeed;
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
