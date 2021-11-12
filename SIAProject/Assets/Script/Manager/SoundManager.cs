using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource AudioSorceForAction;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SoundClip[] soundClips;


    public enum Sound
    {
      BGM,
      PUSH_BUTTON,
      FIRE,
      SKILL_SPEED,
      SKILL_IMMORTAL,
      

    }


  
    [Serializable]
    public struct SoundClip
    {
        public Sound sound;
        public AudioClip audioClip;
        [Range(0, 1)] public float soundVolume;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        PlayBGM(Sound.BGM);
    }



    public void Play(AudioSource audioSource, Sound sound)
    {

        var soundClip = GetAudioClip(sound);
        audioSource.clip = soundClip.audioClip;
        audioSource.volume = soundClip.soundVolume;
        audioSource.Play();
        audioSource.loop = false;

    }

    public void PlayBGM(Sound bgm)
    {
        audioSource.loop = true;
        Play(audioSource, bgm);
    }

    private SoundClip GetAudioClip(Sound sound)
    {
        foreach (var soundClip in soundClips)
        {
            if (soundClip.sound == sound)
            {
                return soundClip;
            }
        }

        return default(SoundClip);
    }

}
