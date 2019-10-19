using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHandle : Handle
{
    //세 개의 라인 받기
    public PlanetLine[] planetLine;
    public PlanetLine otherLine;

    protected override void Start()
    {
        isCatch = false;
    }

    public override void CatchCheck()
    {
        if (isCatch == false)
        {
            isCatch = true;

            //GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>().PlanetReset();
            foreach (PlanetLine line in planetLine)
            {
                if(line.overlapLine.Length != 0)
                {
                    line.GetPlanet(otherLine);
                }
                line.isLock = false;
            }
        }
        else
        {
            isCatch = false;
            //포인트 제자리로 돌리기
            foreach (PlanetLine line in planetLine)
                    line.AngleCheck();

            //if (GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>().PuzzleClearCheck(3))
            //    this.enabled = false;
            
        }
    }

    //핸들잡고 돌리는 부분 캐릭터에게
    public override void HandleRotate(float direction)
    {
        foreach (PlanetLine line in planetLine)
        {
            if(line.isPlanet == true)
            line.Rotate(direction);
        }
    }
}
