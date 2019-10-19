using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPlate : Plate
{
    public int myPuzzleNumber;

    //자동으로 움직이기
    protected override void AutoRotation()
    {
        if (!handle.isCatch)
        {
            if (!isLock)
            {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(transform.localRotation.x, stopAngle[myPoint], transform.localRotation.z), rotateSpeed * 0.5f * Time.deltaTime);
                //원래 위치로 이동이 완료되었으면
                if ((int)transform.localRotation.eulerAngles.y == stopAngle[myPoint])
                {
                    isLock = true;
                    GetComponent<MeshRenderer>().material.SetColor("_TintColor", new Color(1, 0, 0));
                }
            }
        }
    }
}
