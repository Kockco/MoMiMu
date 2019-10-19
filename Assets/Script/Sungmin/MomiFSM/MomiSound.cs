using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomiSound : MonoBehaviour
{
    public AudioClip momiJump;
    public AudioClip[] momiSnow;
    public AudioClip[] momiStone;
    new AudioSource audio;
    public bool isState;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void WalkSoundMomi(bool isSnow)
    {
        int random = Random.Range(0, 3);

        if (isSnow)
            audio.clip = momiSnow[random];
        else
            audio.clip = momiStone[random];

        audio.PlayOneShot(audio.clip);
    }

    public void JumpSoundMomi()
    {
        audio.clip = momiJump;
        audio.PlayOneShot(audio.clip);
    }
}
