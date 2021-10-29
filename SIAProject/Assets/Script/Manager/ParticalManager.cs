using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParticalManager : Singleton<ParticalManager>
{
    private GameManager GM;
    public enum PlayerParticle
    {
        HEALING,
        IMMORTAL,
        ADDDAMAGE,
        SLOW,
        STUNT,
        SPEED,
    }

    [Serializable]
    public struct UseParticle
    {
        public PlayerParticle playerPartical;
        public ParticleSystem partical;
       
    }


    [SerializeField] private UseParticle[] particlesForPlayer;
    private ParticleSystem particle;

    private void Awake()
    {
        GM = GameManager.Instance;
    }
    private void Update()
    {
        transform.position = GM.GetPlayerInSceneTranForm;
    }


    public void PlayParticle(PlayerParticle particalName)
    {
        var particleToPlay = GetPartical(particalName);
        particle = particleToPlay.partical;
        particle.Play();

    }


    public UseParticle GetPartical(PlayerParticle particalName)
    {
        foreach(var particleForPlayer in particlesForPlayer)
        {
            if(particleForPlayer.playerPartical == particalName)
            {
                return particleForPlayer;
            }
            
        }
        return default(UseParticle);
    }

}
