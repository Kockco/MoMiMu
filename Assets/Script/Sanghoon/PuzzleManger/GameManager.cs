using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PuzzleManager effectManager;
    enum PuzzleLevel { StarPuzzle, PotatoPuzzle, PlanetPuzzle, AllClear};
    PuzzleLevel puzzleLevel;

    public bool starPuzzle1Clear;
    public bool starPuzzle2Clear;
    public StarPlate[] starPuzzle1;
    public StarPlate[] starPuzzle2;

    bool potatoPuzzle1Clear;
    bool potatoPuzzle2Clear;
    public PotatoPlate[] potatoPuzzle1;
    public Potato[] potato1;
    public PotatoPlate[] potatoPuzzle2;
    public Potato[] potato2;

    bool planetPuzzleClear;
    public PlanetLine[] planetPuzzle;

    int clearPoint = 0;
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
        clearPoint = 0;
        switch (puzzleLevel)
        {
            case PuzzleLevel.StarPuzzle:
                //퍼즐 1-1클리어 체크
                if (starPuzzle1Clear)
                {
                    for (int i = 0; i < starPuzzle1.Length; i++)
                    {
                        if (starPuzzle1[i].myPoint == 0)
                        {
                            clearPoint++;
                        }
                    }
                    //두개다 맞으면 클리어 이펙트 플레이
                    if (clearPoint == starPuzzle1.Length)
                    {
                        starPuzzle1Clear = true;
                    }
                    clearPoint = 0;
                }
                //퍼즐 1-2클리어 체크
                if (starPuzzle2Clear)
                {
                    //1-2퍼즐
                    for (int i = 0; i < starPuzzle2.Length; i++)
                    {
                        //모든 퍼즐조각이 포인트가 0인가?(0이점답임)
                        if (starPuzzle2[i].myPoint == 0)
                        {
                            clearPoint++;
                        }
                    }
                    //퍼즐2 이펙트 플레이
                    if (clearPoint == starPuzzle2.Length)
                    {
                        starPuzzle2Clear = true;
                    }
                }
                //두개다 클리어 됬다면
                if (starPuzzle2Clear && starPuzzle1Clear) { puzzleLevel = PuzzleLevel.PotatoPuzzle; }
                break;
            case PuzzleLevel.PotatoPuzzle:
                //퍼즐 2-1 클리어 체크
                if (potatoPuzzle1Clear) {
                    for (int i = 0; i < potatoPuzzle1.Length; i++)
                    {
                        if (potatoPuzzle1[i].myPoint == 0)
                        {
                            clearPoint++;
                        }
                    }
                    for (int i = 0; i < potato1.Length; i++)
                    {
                        if (potato1[i].resultNum == -1) { clearPoint++; }
                        else if (potato1[i].myNum == potato1[i].resultNum)
                        {
                            clearPoint++;
                        }
                    }
                    //두개다 맞으면 클리어 이펙트 플레이
                    if (clearPoint == potatoPuzzle1.Length + potato1.Length)
                    {
                        potatoPuzzle1Clear = true;
                    }
                    clearPoint = 0;
                }
                //퍼즐 2-2 클리어 체크
                if (potatoPuzzle2Clear)
                {
                    //퍼즐 모두다 맞았는지 체크
                    for (int i = 0; i < potatoPuzzle2.Length; i++)
                    {
                        if (potatoPuzzle2[i].myPoint == 0)
                        {
                            clearPoint++;
                        }
                    }
                    for (int i = 0; i < potato2.Length; i++)
                    {
                        if (potato2[i].resultNum == -1) { clearPoint++; }
                        else if (potato2[i].myNum == potato2[i].resultNum)
                        {
                            clearPoint++;
                        }
                    }
                    //두개 다 맞으면 클리어 이펙트 플레이
                    if (clearPoint == potatoPuzzle2.Length + potato2.Length)
                    {
                        potatoPuzzle2Clear = true;
                    }
                }
                //두개다 클리어 됬다면
                if (potatoPuzzle1Clear && potatoPuzzle2Clear) { puzzleLevel = PuzzleLevel.PlanetPuzzle; }
                break;
            case PuzzleLevel.PlanetPuzzle:
                
                if (planetPuzzleClear) { puzzleLevel = PuzzleLevel.AllClear; }
                else
                {
                    //퍼즐 다 맞았는지 체크
                    for (int i = 0; i < planetPuzzle.Length; i++)
                    {
                        if (planetPuzzle[i].myPoint == 0)
                        {
                            clearPoint++;
                        }
                    }
                    //전부다 맞으면 클리어 이펙트 플레이
                    if (clearPoint == planetPuzzle.Length)
                    {
                        planetPuzzleClear = true;
                    }
                }

                break;
            case PuzzleLevel.AllClear:
                break;
        }
    }
    //스타 퍼즐 클리어 체크 (1탄퍼즐) 매개변수는 1-1 인지 1-2인지 체크
    public void StarPuzzleClearCheck(int PuzzleNumber)
    {

    }
}
