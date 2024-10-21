using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip []bgm;
    public AudioSource nowBGM;

    public void ChangeBGM(int index)
    {
        nowBGM.Stop();
        nowBGM.clip = bgm[index];
        nowBGM.Play();
    }
}
