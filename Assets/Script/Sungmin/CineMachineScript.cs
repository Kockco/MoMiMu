using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
using Cinemachine;

public class CineMachineScript : MonoBehaviour
{
    public GameManager game;
    public GameObject[] starPuzzles;
    public GameObject[] potatoPuzzles;
    public GameObject[] planetPuzzles;
    public GameObject fade;
    GameObject cam;

    public float cineTimer;
    bool switchingCam = false;
    bool cineTrigger;

    // Start is called before the first frame update
    void Start()
    {
        InitAllPuzzle();

        cam = Camera.main.transform.gameObject;
    }

    void Update()
    {
        if (cineTrigger)
            cineTimer += Time.deltaTime;
    }

    public void InitAllPuzzle()
    {
        fade.SetActive(false);

        for (int i = 0; i < starPuzzles.Length; i++)
            starPuzzles[i].SetActive(false);

        for (int i = 0; i < potatoPuzzles.Length; i++)
            potatoPuzzles[i].SetActive(false);
    }

    public void CineCameraSwitching()
    {
        if (!switchingCam)
        {
            fade.SetActive(true);
            cam.GetComponent<Camera>().enabled = false;
        }
        else
        {
            fade.SetActive(false);
            cam.GetComponent<Camera>().enabled = true;
        }
    }

    public void PlayPuzzleCine(int puzzleNum, float timer)
    {
        cineTrigger = true;

        if (cineTimer >= timer)
        {
            fade.SetActive(true);

            switch (puzzleNum)
            {
                case 1:
                    CineCameraSwitching();
                    for (int i = 0; i < starPuzzles.Length; i++)
                        starPuzzles[i].SetActive(true);

                    Invoke("EndTimeLine", 23);
                    break;
                case 2:
                    CineCameraSwitching();
                    for (int i = 0; i < potatoPuzzles.Length; i++)
                        potatoPuzzles[i].SetActive(true);

                    Invoke("EndTimeLine", 21);
                    break;
            }
        }
    }

    public void EndTimeLine()
    {
        switchingCam = true;
        CineCameraSwitching();
        InitAllPuzzle();
        cineTrigger = false;
        cineTimer = 0;

        // Camera.main.transform.gameObject.SetActive(true);
        // GetComponent<GameObject>().SetActive(false);

        if (game.puzzleLevel == GameManager.PuzzleLevel.StarPuzzle && game.starPuzzle1Clear)
            game.puzzleLevel = GameManager.PuzzleLevel.PotatoPuzzle;

        if (game.puzzleLevel == GameManager.PuzzleLevel.PotatoPuzzle && game.potatoPuzzle1Clear)
            game.puzzleLevel = GameManager.PuzzleLevel.PlanetPuzzle;

        if (game.puzzleLevel == GameManager.PuzzleLevel.PlanetPuzzle && game.planetPuzzleClear)
            game.puzzleLevel = GameManager.PuzzleLevel.AllClear;

    }

}
