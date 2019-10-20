using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EffectManager : MonoBehaviour
{
    
    //1번퍼즐
    #region
    public ParticleSystem[] starPuzzleParticle1;
    public ParticleSystem[] starPuzzleClearEffect1;
    public ParticleSystem[] starPuzzleParticle2;
    public ParticleSystem[] starPuzzleClearEffect2;
    public ParticleSystem[] starPuzzleAllClearEffect;
    #endregion

    //2번퍼즐
    #region

    public ParticleSystem[] potatoPuzzleParticle;
    public ParticleSystem[] potatoPuzzleParticle2;
    public ParticleSystem[] potatoPuzzleClearEffect;
    public ParticleSystem[] potatoPuzzleClearEffect2;
    public ParticleSystem[] potatoPuzzleAllClearEffect;

    #endregion

    //3번퍼즐
    #region
    public ParticleSystem[] planetPuzzleParticle;
    public ParticleSystem[] planetPuzzleLineParticle;
    public ParticleSystem[] planetPuzzleAllClearEffect;
    #endregion

    // 시네머신
    #region
    CameraScript cam;
    public bool isPuzzleClear;
    public int viewNum;
    #endregion

    void Start()
    {
        cam = Camera.main.transform.gameObject.GetComponent<CameraScript>();
    }

    void Update()
    {
        if (isPuzzleClear)
        {
            cam.PuzzleClearView(cam.moveToObject[viewNum].transform.GetChild(0).gameObject);

            Invoke("PuzzleViewCamOff", 3.5f);
        }
    }

    void PuzzleViewCamOff()
    {
        cam.puzzleClear = false;
        isPuzzleClear = false;
    }

    public void PuzzleClearCheck(int PuzzleNumber)
    {
        switch (PuzzleNumber)
        {
            // ///////////////////////////////// Star Puzzle
            case 1:
                foreach (ParticleSystem effect in starPuzzleParticle1)
                    effect.Play();
                break;
            case 2:
                foreach (ParticleSystem effect in starPuzzleClearEffect1)
                    effect.Play();

                viewNum = 0;
                isPuzzleClear = true;
                break;
            case 3:
                foreach (ParticleSystem effect in starPuzzleParticle2)
                    effect.Play();
                break;
            case 4:
                foreach (ParticleSystem effect in starPuzzleClearEffect2)
                    effect.Play();

                viewNum = 1;
                isPuzzleClear = true;
                break;
            // ///////////////////////////////// Potato Puzzle
            case 5:
                // foreach (ParticleSystem effect in potatoPuzzleParticle) effect.Play();
                break;
            case 6:
                foreach (ParticleSystem effect in potatoPuzzleClearEffect)
                    effect.Play();

                viewNum = 2;
                isPuzzleClear = true;
                break;
            case 7:
                // foreach (ParticleSystem effect in potatoPuzzleParticle2) effect.Play();
                break;
            case 8:
                foreach (ParticleSystem effect in potatoPuzzleClearEffect2)
                    effect.Play();

                viewNum = 3;
                isPuzzleClear = true;
                break;
            // ////////////////////////////////// Planet Puzzle
            case 9:
                planetPuzzleParticle[PuzzleNumber].Play();
                break;
            
        }
    }

    public void PuzzleAllClearEffect(int puzzleNum)
    {
        switch(puzzleNum)
        {
            case 1:
                foreach (ParticleSystem effect in starPuzzleAllClearEffect)
                    effect.Play();
                break;
            case 2:
                foreach (ParticleSystem effect in potatoPuzzleAllClearEffect)
                    effect.Play();
                break;
            case 3:
                foreach (ParticleSystem effect in planetPuzzleAllClearEffect)
                    effect.Play();
                break;
        }
    }
}