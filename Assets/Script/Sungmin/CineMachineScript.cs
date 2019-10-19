﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
using Cinemachine;

public class CineMachineScript : MonoBehaviour
{
    public GameObject[] starPuzzles;
    public GameObject[] potatoPuzzles;
    public GameObject[] planetPuzzles;
    public PlayableDirector play;
    public TimelineAsset[] timeline;

    bool switchingCam = false;

    // Start is called before the first frame update
    void Start()
    {
        InitStarPuzzle();
    }

    public void InitStarPuzzle()
    {
        for (int i = 0; i < starPuzzles.Length; i++)
            starPuzzles[i].SetActive(false);

        for (int i = 0; i < potatoPuzzles.Length; i++)
            potatoPuzzles[i].SetActive(false);
    }

    public void CineCameraSwitching()
    {
        if (!switchingCam)
        {
            GetComponent<GameObject>().SetActive(true);
            Camera.main.transform.gameObject.SetActive(false);
        }
        else
        {
            Camera.main.transform.gameObject.SetActive(true);
            GetComponent<GameObject>().SetActive(false);
        }
    }

    public void PlayPuzzleCine(int puzzleNum)
    {
        switch(puzzleNum)
        {
            case 1:
                break;
        }
    }

    public void ClearPuzzleCine(int puzzleNum)
    {
        switchingCam = true;
        play.playableAsset = timeline[puzzleNum - 1];
        play.Play();
    }

    public void EndTimeLine()
    {
        switchingCam = false;
        Camera.main.transform.gameObject.SetActive(true);
        GetComponent<GameObject>().SetActive(false);
    }

}
