using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    [Header("Property")]
    public string Name;
    public Image SkillImage;
    public Image SkillButtonImg;

    [Header("About Skill")]
    public float CoolDownSkill = 0;
    public float timeOfSkill;
    public int SkillPrice;

    private ParticalManager particleManager;
    [SerializeField]private ParticalManager.PlayerParticle SkillParticle;

    private bool isCoolDown = false;
    private void Awake()
    {
        particleManager = ParticalManager.Instance;
        
    }
    //protected float timeCount = 0;
    // [SerializeField] protected ParticleSystem Particle;

  

    public virtual void AboutSkill(PlayerCharecter player)
    {
        UseParticle(SkillParticle);
    }

    //Add particle to skill
    private void UseParticle(ParticalManager.PlayerParticle particle)
    {
        particleManager.PlayParticle(particle);
    }


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
