using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Singleton<SoundManager>
{
    public AudioSource audioSource;
    public AudioSource bgMusic;
    public override void InitAwake()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateMusicBG();
    }

    [Header("List audioClip UI")]
    public List<AudioClip> audioClips;



    /// <summary>
    /// 0. Click - 
    /// </summary>
    /// <param name="type"></param>
    public void UpdateMusicBG()
    {
        if (Module.music_fx == 1)
        {
            bgMusic.Play();
        }
        else
        {
            bgMusic.Stop();
        }
    }
    public void PlayFx(int type = 0)
    {
        audioSource.volume = Module.sound_fx;
        audioSource.clip = audioClips[type];
        audioSource.Play();
    }

    public void PlayOnCamera(int type = 0)
    {
        AudioSource.PlayClipAtPoint(audioClips[type], Camera.main.transform.position, Module.sound_fx);
    }

    public void PlayOnCamera(AudioClip _clip)
    {
        AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, Module.sound_fx);
    }


    public bool IsFxPlaying()
    {
        return audioSource.isPlaying;
    }
}
