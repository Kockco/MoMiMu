using System.Collections;
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

    #region
    public Material[] core;
    Color color1;
    Color color2;
    Color color3;
    public Material[] handleMat;
    Color handleColor1;
    Color handleColor2;
    Color handleColor3;
    Color handleColor4;
    Color handleColor5;
    #endregion

    void Start()
    {
        color1 = new Color(0.27f, 0.27f, 0.27f);
        color2 = new Color(0.27f, 0.27f, 0.27f);
        color3 = new Color(0.27f, 0.27f, 0.27f);
        handleColor1 = new Color(1.26f, 1.01f, 0.8f);
        handleColor2 = new Color(1.26f, 1.01f, 0.8f);
        handleColor3 = new Color(1.15f, 1.15f, 1.15f);
        handleColor4 = new Color(1.15f, 1.15f, 1.15f);
        handleColor5 = new Color(1.15f, 1.15f, 1.15f);
        foreach (Material a in core)
        {
            a.SetColor("_EmissionColor", color1);
        }
        for (int i = 0; i < 2; i++) { handleMat[i].SetColor("_EmissionColor", handleColor1); }
        for (int i = 2; i < 5; i++) { handleMat[i].SetColor("_EmissionColor", handleColor3); }
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
        ChangeCoreEmission();
        ChangeHandleEmission();
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
                    }
                }
                else
                    cine.PlayPuzzleCine(3, 3.5f);

                break;
            case PuzzleLevel.AllClear:
                // SceneManager.LoadScene(0); (메인화면)
                break;
        }
    }
    void ChangeCoreEmission()
    {
        if (starPuzzle1Clear && starPuzzle2Clear)
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
        if (potatoPuzzle1Clear && potatoPuzzle2Clear)
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
        if (planetPuzzleClear)
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
    void ChangeHandleEmission()
    {
        if (starPuzzle1Clear)
        {
            if (handleMat[0].GetColor("_EmissionColor").r <= 3.26f ||
                handleMat[0].GetColor("_EmissionColor").g <= 3.01f ||
                handleMat[0].GetColor("_EmissionColor").b <= 2.8f)
            {
                if (handleColor1.r < 3.26f)
                    handleColor1.r += 1f * Time.deltaTime;
                if (handleColor1.g < 3.01f)
                    handleColor1.g += 1f * Time.deltaTime;
                if (handleColor1.b < 2.8f)
                    handleColor1.b += 1f * Time.deltaTime;

                handleMat[0].SetColor("_EmissionColor", handleColor1);
            }
        }
        if (starPuzzle2Clear)
        {
            if (handleMat[1].GetColor("_EmissionColor").r <= 3.26f ||
                handleMat[1].GetColor("_EmissionColor").g <= 3.01f ||
                handleMat[1].GetColor("_EmissionColor").b <= 2.8f)
            {
                if (handleColor2.r < 3.26f)
                    handleColor2.r += 1f * Time.deltaTime;
                if (handleColor2.g < 3.01f)
                    handleColor2.g += 1f * Time.deltaTime;
                if (handleColor2.b < 2.8f)
                    handleColor2.b += 1f * Time.deltaTime;

                handleMat[1].SetColor("_EmissionColor", handleColor2);
            }
        }
        if (potatoPuzzle1Clear)
        {
            if (handleMat[2].GetColor("_EmissionColor").r <= 5.15f ||
                handleMat[2].GetColor("_EmissionColor").g <= 5.15f ||
                handleMat[2].GetColor("_EmissionColor").b <= 5.15f)
            {
                if (handleColor3.r < 5.15f)
                    handleColor3.r += 1f * Time.deltaTime;
                if (handleColor3.g < 5.15f)
                    handleColor3.g += 1f * Time.deltaTime;
                if (handleColor3.b < 5.15f)
                    handleColor3.b += 1f * Time.deltaTime;

                handleMat[2].SetColor("_EmissionColor", handleColor3);
            }
        }
        if (potatoPuzzle2Clear)
        {
            if (handleMat[3].GetColor("_EmissionColor").r <= 5.15f ||
                handleMat[3].GetColor("_EmissionColor").g <= 5.15f ||
                handleMat[3].GetColor("_EmissionColor").b <= 5.15f)
            {
                if (handleColor4.r < 5.15f)
                    handleColor4.r += 1f * Time.deltaTime;
                if (handleColor4.g < 5.15f)
                    handleColor4.g += 1f * Time.deltaTime;
                if (handleColor4.b < 5.15f)
                    handleColor4.b += 1f * Time.deltaTime;

                handleMat[3].SetColor("_EmissionColor", handleColor4);
            }
        }

        if (planetPuzzleClear)
        {
            if (handleMat[4].GetColor("_EmissionColor").r <= 5.15f ||
                handleMat[4].GetColor("_EmissionColor").g <= 5.15f ||
                handleMat[4].GetColor("_EmissionColor").b <= 5.15f)
            {
                if (handleColor5.r < 5.15f)
                    handleColor5.r += 1f * Time.deltaTime;
                if (handleColor5.g < 5.15f)
                    handleColor5.g += 1f * Time.deltaTime;
                if (handleColor5.b < 5.15f)
                    handleColor5.b += 1f * Time.deltaTime;

                handleMat[4].SetColor("_EmissionColor", handleColor5);
            }
        }
    }
}