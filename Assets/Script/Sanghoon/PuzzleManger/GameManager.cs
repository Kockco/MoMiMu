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
    public bool stepClear;
    public PlanetLine[] planetPuzzle;

    public int clearPoint1 = 0;
    public int clearPoint2 = 0;

    public Material[] core;
    Color color1;
    Color color2;
    Color color3;
    void Start()
    {
        color1 = new Color(0.27f, 0.27f, 0.27f);
        color2 = new Color(0.27f, 0.27f, 0.27f);
        color3 = new Color(0.27f, 0.27f, 0.27f);
        foreach (Material a in core)
        {
            a.SetColor("_EmissionColor", color1);
        }
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
        if (puzzleLevel == PuzzleLevel.PotatoPuzzle)
        {
            if (core[0].GetColor("_EmissionColor").r <= 2.77f ||
                core[0].GetColor("_EmissionColor").g <= 3.02f ||
                core[0].GetColor("_EmissionColor").b <= 3.24f)
            {
                if (color1.r < 2.77f)
                    color1.r += 1f * Time.deltaTime;
                if (color1.g < 3.02f)
                    color1.g += 1f * Time.deltaTime;
                if (color1.b < 3.24f)
                    color1.b += 1f * Time.deltaTime;

                core[0].SetColor("_EmissionColor", color1);
            }
        }
        if (puzzleLevel == PuzzleLevel.PlanetPuzzle)
        {
            if (core[1].GetColor("_EmissionColor").r <= 2.77f ||
                core[1].GetColor("_EmissionColor").g <= 3.02f ||
                core[1].GetColor("_EmissionColor").b <= 3.24f)
            {
                if (color2.r < 2.77f)
                    color2.r += 0.25f * Time.deltaTime;
                if (color2.g < 3.02f)
                    color2.g += 0.25f * Time.deltaTime;
                if (color2.b < 3.24f)
                    color2.b += 0.25f * Time.deltaTime;
                core[1].SetColor("_EmissionColor", color2);
            }
        }
        if (puzzleLevel == PuzzleLevel.AllClear)
        {
            if (core[2].GetColor("_EmissionColor").r <= 2.77f ||
                core[2].GetColor("_EmissionColor").g <= 3.02f ||
                core[2].GetColor("_EmissionColor").b <= 3.24f)
            {
                if (color3.r < 2.77f)
                    color3.r += 0.25f * Time.deltaTime;
                if (color3.g < 3.02f)
                    color3.g += 0.25f * Time.deltaTime;
                if (color3.b < 3.24f)
                    color3.b += 0.25f * Time.deltaTime;
                core[2].SetColor("_EmissionColor", color3);
            }
        }
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
                        if (starPuzzle1[i].myPoint == 0 && !stepClear)
                        {
                            clearPoint1++;
                            stepClear = true;
                            effectManager.PuzzleClearCheck(1);
                        }
                        else
                            stepClear = false;
                    }
                    //두개다 맞으면 클리어 이펙트 플레이
                    if (clearPoint1 == starPuzzle1.Length)
                    {
                        starPuzzle1Clear = true;
                        stepClear = false;
                        effectManager.PuzzleClearCheck(2);
                    }
                }
                //퍼즐 1-2클리어 체크
                if (!starPuzzle2Clear)
                {
                    //1-2퍼즐
                    for (int i = 0; i < starPuzzle2.Length; i++)
                    {
                        //모든 퍼즐조각이 포인트가 0인가?(0이점답임)
                        if (starPuzzle2[i].myPoint == 0 && !stepClear)
                        {
                            clearPoint2++;
                            stepClear = true;
                            effectManager.PuzzleClearCheck(3);
                        }
                        else
                            stepClear = false;
                    }
                    //퍼즐2 이펙트 플레이
                    if (clearPoint2 == starPuzzle2.Length)
                    {
                        starPuzzle2Clear = true;
                        stepClear = false;
                        effectManager.PuzzleClearCheck(4);
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
                        if (potatoPuzzle1[i].myPoint == 0 && !stepClear)
                        {
                            clearPoint1++;
                            stepClear = true;
                        }
                        else
                            stepClear = false;
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
                        stepClear = false;
                        effectManager.PuzzleClearCheck(6);
                        clearPoint1 = 0;
                    }
                }
                //퍼즐 2-2 클리어 체크
                if (!potatoPuzzle2Clear)
                {
                    //퍼즐 모두다 맞았는지 체크
                    for (int i = 0; i < potatoPuzzle2.Length; i++)
                    {
                        if (potatoPuzzle2[i].myPoint == 0 && !stepClear)
                        {
                            clearPoint2++;
                            stepClear = true;
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
                        stepClear = false;
                        effectManager.PuzzleClearCheck(8);
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
                            stepClear = true;
                        }
                        else
                            stepClear = false;
                    }
                    //전부다 맞으면 클리어 이펙트 플레이
                    if (clearPoint1 == planetPuzzle.Length)
                    {
                        planetPuzzleClear = true;
                        stepClear = false;
                        effectManager.PuzzleClearCheck(12);
                        effectManager.PuzzleAllClearEffect(3);
                        clearPoint1 = 0;
                    }
                }
                break;
            case PuzzleLevel.AllClear:
                if (stepClear)
                    SceneManager.LoadScene(3);
                break;
        }
    }
}