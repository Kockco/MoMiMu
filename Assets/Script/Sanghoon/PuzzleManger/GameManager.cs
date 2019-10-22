﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public EffectManager effectManager;
    public CineMachineScript cine;
    public enum PuzzleLevel { StarPuzzle, PotatoPuzzle, PlanetPuzzle, AllClear };
    public PuzzleLevel puzzleLevel;

    public bool starPuzzle1Clear;
    bool starPuzzle2Clear;
    public StarPlate[] starPuzzle1;
    public StarPlate[] starPuzzle2;

    public bool potatoPuzzle1Clear;
    bool potatoPuzzle2Clear;
    public PotatoPlate[] potatoPuzzle1;
    public Potato[] potato1;
    public PotatoPlate[] potatoPuzzle2;
    public Potato[] potato2;

    public bool planetPuzzleClear;
    public PlanetLine[] planetPuzzle;

    public int clearPoint1 = 0;
    public int clearPoint2 = 0;
    public int effectShot;

    void Start()
    {
        starPuzzle1Clear = false;
        starPuzzle2Clear = false;
        potatoPuzzle1Clear = false;
        potatoPuzzle2Clear = false;
        planetPuzzleClear = false;
        Application.targetFrameRate = 40;
        puzzleLevel = PuzzleLevel.StarPuzzle;
    }

    void Update()
    {
        PuzzleClearCheck();
    }

    void PuzzleClearCheck()
    {
        clearPoint1 = 0; clearPoint2 = 0;
        switch (puzzleLevel)
        {
            case PuzzleLevel.StarPuzzle:
                //퍼즐 1-1클리어 체크
                if (!starPuzzle1Clear)
                {
                    for (int i = 0; i < starPuzzle1.Length; i++)
                    {
                        if (starPuzzle1[i].myPoint == 0)
                        {
                            clearPoint1++;
                            //starPuzzle1[i].myPoint = -1;
                            if (effectShot == 0)
                            {
                                effectManager.PuzzleClearCheck(1);
                                effectShot++;
                            }
                        }
                    }
                    //두개다 맞으면 클리어 이펙트 플레이
                    if (clearPoint1 == starPuzzle1.Length)
                    {
                        starPuzzle1Clear = true;
                        if (effectShot == 1)
                        {
                            effectManager.PuzzleClearCheck(2);
                            effectShot++;
                        }
                    }
                }
                //퍼즐 1-2클리어 체크
                if (!starPuzzle2Clear)
                {
                    //1-2퍼즐
                    for (int i = 0; i < starPuzzle2.Length; i++)
                    {
                        //모든 퍼즐조각이 포인트가 0인가?(0이점답임)
                        if (starPuzzle2[i].myPoint == 0)
                        {
                            clearPoint2++;
                            //starPuzzle2[i].myPoint = -1;
                            if (effectShot == 2)
                            {
                                effectManager.PuzzleClearCheck(3);
                                effectShot++;
                            }
                        }
                    }
                    //퍼즐2 이펙트 플레이
                    if (clearPoint2 == starPuzzle2.Length)
                    {
                        starPuzzle2Clear = true;
                        if (effectShot == 3)
                        {
                            effectManager.PuzzleClearCheck(4);
                            effectShot++;
                        }
                        clearPoint2 = 0;
                    }
                }
                //두개다 클리어 됬다면
                if (starPuzzle2Clear && starPuzzle1Clear)
                {
                    effectManager.PuzzleAllClearEffect(1);
                    cine.PlayPuzzleCine(1, 3.5f);
                }
                break;

            case PuzzleLevel.PotatoPuzzle:
                //퍼즐 2-1 클리어 체크
                if (!potatoPuzzle1Clear)
                {
                    for (int i = 0; i < potatoPuzzle1.Length; i++)
                    {
                        if (potatoPuzzle1[i].myPoint == 0)
                        {
                            clearPoint1++;
                        }
                    }
                    for (int i = 0; i < potato1.Length; i++)
                    {
                        if (potato1[i].resultNum == -1) { clearPoint1++; }
                        else if (potato1[i].myNum == potato1[i].resultNum)
                        {
                            clearPoint1++;
                        }
                    }
                    //두개다 맞으면 클리어 이펙트 플레이
                    if (clearPoint1 == potatoPuzzle1.Length + potato1.Length)
                    {
                        potatoPuzzle1Clear = true;
                        if (effectShot == 4)
                        {
                            effectManager.PuzzleClearCheck(6);
                            effectShot++;
                        }
                        clearPoint1 = 0;
                    }
                }
                //퍼즐 2-2 클리어 체크
                if (!potatoPuzzle2Clear)
                {
                    //퍼즐 모두다 맞았는지 체크
                    for (int i = 0; i < potatoPuzzle2.Length; i++)
                    {
                        if (potatoPuzzle2[i].myPoint == 0)
                        {
                            clearPoint2++;
                        }
                    }
                    for (int i = 0; i < potato2.Length; i++)
                    {
                        if (potato2[i].resultNum == -1) { clearPoint2++; }
                        else if (potato2[i].myNum == potato2[i].resultNum)
                        {
                            clearPoint2++;
                        }
                    }
                    //두개 다 맞으면 클리어 이펙트 플레이
                    if (clearPoint2 == potatoPuzzle2.Length + potato2.Length)
                    {
                        potatoPuzzle2Clear = true;
                        if (effectShot == 5)
                        {
                            effectManager.PuzzleClearCheck(8);
                            effectShot++;
                        }
                        clearPoint2 = 0;
                    }
                }
                //두개다 클리어 됬다면
                if (potatoPuzzle1Clear && potatoPuzzle2Clear)
                {
                    effectManager.PuzzleAllClearEffect(2);
                    cine.PlayPuzzleCine(2, 3.5f);
                }
                break;

            case PuzzleLevel.PlanetPuzzle:
                if (!planetPuzzleClear)
                {
                    //퍼즐 다 맞았는지 체크
                    for (int i = 0; i < planetPuzzle.Length; i++)
                    {
                        if (planetPuzzle[i].myPoint == 0)
                        {
                            if (planetPuzzle[0].myPoint == 0)
                                effectManager.PuzzleClearCheck(9);
                            if (planetPuzzle[1].myPoint == 0)
                                effectManager.PuzzleClearCheck(10);
                            if (planetPuzzle[2].myPoint == 0)
                                effectManager.PuzzleClearCheck(11);

                            clearPoint1++;
                        }
                        if (planetPuzzle[0].myPoint != 0)
                            effectManager.planetPuzzleParticle1[0].Stop();
                        if (planetPuzzle[1].myPoint != 0)
                            effectManager.planetPuzzleParticle2[0].Stop();
                        if (planetPuzzle[2].myPoint != 0)
                            effectManager.planetPuzzleParticle3[0].Stop();
                    }
                    //전부다 맞으면 클리어 이펙트 플레이
                    if (clearPoint1 == planetPuzzle.Length)
                    {
                        planetPuzzleClear = true;
                        effectManager.PuzzleClearCheck(12);
                        effectManager.PuzzleAllClearEffect(3);
                        clearPoint1 = 0;
                    }
                }

                break;
            case PuzzleLevel.AllClear:
                // SceneManager.LoadScene(0); (메인화면)
                break;
        }
    }
}