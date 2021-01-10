using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    //Add these to their respective scripts:

    //Game Logic - > Add private void awake:     void Awake(){SoundManager.Initialize();}

    // Player_Control -> all move calls:         SoundManager.PlaySound(SoundManager.Sound.PlayerSustainedMove, transform.position);   Needs Audio: SustainedEngine
    // Module - > ontriggerenter2d :             SoundManager.PlaySound(SoundManager.Sound.Selection, transform.position);             Needs Audio: Selection
    // Bullet - > Inside of player hit:          SoundManager.PlaySound(SoundManager.Sound.PlayerDead, transform.position);            Needs Audio: 
    // Bullet - > Inside of Boss hit:            SoundManager.PlaySound(SoundManager.Sound.EnemyHit, transform.position);              Needs Audio: 
    // Bullet - > In start inside "If rocket":   SoundManager.PlaySound(SoundManager.Sound.RocketFire, transform.position);            Needs Audio: 
    // Bullet - > In start "If rocket..."else:{  SoundManager.PlaySound(SoundManager.Sound.GunFire, transform.position);}              Needs Audio: 
    // Bullet - > Inside of Enable explosion:    SoundManager.PlaySound(SoundManager.Sound.Explosion, transform.position);             Needs Audio: 
    //
    //
    public enum Sound
    {
        PlayerMove,             //at the beginning of the move
        PlayerSustainedMove,    //while the move is in progress
        PlayerHit,              //on player taking damage
        PlayerDead,             //when they die
        EnemyHit,               //when they take a hit
        EnemyDead,              //boss lost a module or DEATH 
        PickUpModule,           //player picks up module
        Selection,              //player selects different module
        Explosion,              //when an explosion happens
        GunFire,                //on gun fire
        BurstFire,              //on burst fire
        RocketFire,             //when you shoot the bfROCKET
        BossDead,               //when boss dies
    }

    private static Dictionary<Sound, float> soundTimerDictionary;


    //Will need to call this in the gamehandler as apart of the awake function

    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerSustainedMove] = 0;
    }

    //


    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(GetAudioClip(sound));
        }        
    }

    public static void PlaySound(Sound sound, Vector3 position)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.maxDistance = 100f;
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            audioSource.Play();


            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            case Sound.PlayerSustainedMove:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = 1.30f;
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }   
                }
                else
                {
                    return true;
                }
                //break;
        }
       
    }


    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.i.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
        
    }
}
