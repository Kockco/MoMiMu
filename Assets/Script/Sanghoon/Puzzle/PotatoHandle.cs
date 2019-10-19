﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoHandle : Handle
{
    protected override void Start()
    {
        isCatch = false;
    }

    public override void CatchCheck()
    {
        if (isCatch == false)
        {
            isCatch = true;
            plate.isLock = false;
            plate.GetComponent<PotatoPlate>().GetPotatoChild();
        }
        else
        {
            isCatch = false;
            //포인트 제자리로 돌리기
            plate.AngleCheck();
        }
    }

    //핸들잡고 돌리는 부분 캐릭터에게
    public override void HandleRotate(float direction)
    {
        plate.Rotate(direction);
    }
}
