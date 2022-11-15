using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager instance;

    public AudioSource bgmPlayer;
    public AudioSource[] sfxPlayer;
    public int nextPlayer;

    public AudioClip startClip;
    public AudioClip overClip;
    public AudioClip FailClip;
    public AudioClip[] hitClip;
    void Start()
    {
        instance = this;
    }

    public static void BgmStart()
    {
        instance.bgmPlayer.Play();
    }
    public static void BgmStop()
    {
        instance.bgmPlayer.Stop();
    }
    public static void PlaySound(string name)
    {
        switch (name)
        {
            case "Start":
                instance.sfxPlayer[instance.nextPlayer].clip = instance.startClip;
                break;
            case "Over":
                instance.sfxPlayer[instance.nextPlayer].clip = instance.overClip;
                break;
            case "Hit":
                int ran = Random.Range(0, instance.hitClip.Length);
                instance.sfxPlayer[instance.nextPlayer].clip = instance.hitClip[ran];
                break;
            case "Fail":
                instance.sfxPlayer[instance.nextPlayer].clip = instance.FailClip;
                break;

        }
        instance.sfxPlayer[instance.nextPlayer].Play();
        instance.nextPlayer = (instance.nextPlayer + 1) % instance.sfxPlayer.Length;
    }
}
