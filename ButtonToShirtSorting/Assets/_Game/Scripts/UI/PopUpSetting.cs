using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSetting : MonoBehaviour
{
    [SerializeField] UIButton btnClose;
    [SerializeField] UIButton btnSound;
    [SerializeField] UIButton btnMusic;
    [SerializeField] UIButton btnHome;
    public void CallStart()
    {
        CheckSoundMusic();
        btnSound.SetUpEvent(Action_btnSound);
        btnMusic.SetUpEvent(Action_btnMusic);
        btnClose.SetUpEvent(Action_btnClose);
        btnHome.SetUpEvent(Action_btnHome);
    }
    private void CheckSoundMusic()
    {
        if (Module.sound_fx == 1)
        {
            btnSound.GetComponent<Image>().color = Color.white;
        }
        else
        {
            btnSound.GetComponent<Image>().color = Color.black;
        }
        if (Module.music_fx == 1)
        {
            btnMusic.GetComponent<Image>().color = Color.white;
        }
        else
        {
            btnMusic.GetComponent<Image>().color = Color.black;
        }
    }
    private void Action_btnMusic()
    {
        if (Module.music_fx == 1)
        {
            Module.music_fx = 0;
        }
        else
        {
            Module.music_fx = 1;
        }
        CheckSoundMusic();
        SoundManager.Instance.UpdateMusicBG();
    }

    private void Action_btnSound()
    {
        if (Module.sound_fx == 1)
        {
            Module.sound_fx = 0;
        }
        else
        {
            Module.sound_fx = 1;
        }
        CheckSoundMusic();
    }

    private void Action_btnHome()
    {
        OnClose();
        GameController.Instance.ResetLevel();
        UIManager.Instance.ShowUIHome();
    }

    private void Action_btnClose()
    {
        OnClose();
    }

    private void OnClose()
    {
        this.gameObject.SetActive(false);
    }
}
