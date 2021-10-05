using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public string Name;
    public float CoolDownSkill = 0;
    public float timeOfSkill;
    //protected float timeCount = 0;
    // [SerializeField] protected ParticleSystem Particle;

    public abstract void AboutSkill(PlayerCharecter player);
    protected abstract void EndSkill(PlayerCharecter player);

    //protected virtual void CoolDown(float time)
    //{
    //    time = timeOfSkill;
    //    if (time >= 0) return;
    //    while(time<=0)
    //    {
    //        time -= Time.deltaTime;
    //    }
    //    return;
    //    //End Skill or Can Use Skill
    //}

   
}
