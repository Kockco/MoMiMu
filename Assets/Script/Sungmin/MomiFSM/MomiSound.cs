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

    int stepCount = 0;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void WalkSoundMomi(bool isSnow)
    {
        if (isSnow)
            audio.clip = momiSnow[stepCount];
        else
            audio.clip = momiStone[stepCount];

        audio.PlayOneShot(audio.clip);
        stepCount++;

        if (stepCount >= momiSnow.Length)
            stepCount = 0;
    }

    public void JumpSoundMomi()
    {
        audio.clip = momiJump;
        audio.PlayOneShot(audio.clip);
    }
}
