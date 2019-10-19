using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLine : Plate
{
    [Header("행성번호")]
    public int planetNumber;

    [Header("자식")]
    [SerializeField]
    GameObject planet;
    
    [Header("자전속도")]
    [SerializeField]
    float selfRotateSpeed;

    [Header("Z로 돌아야되는가?")]
    [SerializeField]
    bool isRotationZ;

    [Header("겹쳐지는 라인 갯수,지점")]
    public int[] overlapLine;

    [Header("현재 라인에 행성이 존재하는가?")]
    public bool isPlanet;

    protected override void Start()
    {
        stopAngle = new float[cutAngle];
        centerAngle = new float[cutAngle];
        isLock = true;

        //360 / 잘린갯수만큼 계산
        float startAngle = 360 / cutAngle;
        for (int i = 0; i < cutAngle; i++)
        {
            stopAngle[i] = i * startAngle;
            centerAngle[i] = (startAngle / 2) + startAngle * i;
        }

        if (isRotationZ)
        {
            Quaternion startingAngleY = Quaternion.Euler(0, 0, stopAngle[myPoint]);
            transform.localRotation = startingAngleY;
        }
        else
        {
            Quaternion startingAngleY = Quaternion.Euler(0, stopAngle[myPoint],0);
            transform.localRotation = startingAngleY;
        }

        //행성이 있으면 true 없으면 false
        if (transform.childCount == 0) isPlanet = false;
        else isPlanet = true;
    }
    //회전
    public override void Rotate(float direction)
    {
        if (isRotationZ)
        {
            transform.Rotate(0, 0, rotateSpeed * direction * Time.deltaTime);
            ChildRotation(direction);
        }
        else
        { 
            transform.Rotate(0, rotateSpeed * direction * Time.deltaTime, 0);
            ChildRotation(direction);
        }
    }

    //왼쪽으로 맟출건지 오른쪽으로 맟출건지 결정
    public override void AngleCheck()
    {
        if (isRotationZ)
        {
            float ang = transform.localRotation.eulerAngles.z;

            for (int i = 0; i < cutAngle; i++)
            {
                if (i == cutAngle - 1)
                {
                    if (ang > centerAngle[i] || ang < centerAngle[0])
                    {
                        myPoint = 0;
                    }
                }
                else
                {
                    if (ang > centerAngle[i] && ang < centerAngle[i + 1])
                    {
                        myPoint = i + 1;
                    }
                }
            }
        }
        else
        {
            float ang = transform.localRotation.eulerAngles.y;

            for (int i = 0; i < cutAngle; i++)
            {
                if (i == cutAngle - 1)
                {
                    if (ang > centerAngle[i] || ang < centerAngle[0])
                    {
                        myPoint = 0;
                    }
                }
                else
                {
                    if (ang > centerAngle[i] && ang < centerAngle[i + 1])
                    {
                        myPoint = i + 1;
                    }
                }
            }
        }
    }

    //최대값 최소값 지정
    protected override void AngleLimit()
    {
        if (isRotationZ)
        {
            if (transform.localRotation.eulerAngles.y > 360)
            {
                transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,
                    transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z-360);
            }
            if (transform.localRotation.eulerAngles.y < 0)
            {
                transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,
                    transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z+360);
            }
        }
        else
        {
            if (transform.localRotation.eulerAngles.y > 360)
            {
                transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,
                    transform.localRotation.eulerAngles.y - 360, transform.localRotation.eulerAngles.z);
            }
            if (transform.localRotation.eulerAngles.y < 0)
            {
                transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,
                    transform.localRotation.eulerAngles.y + 360, transform.localRotation.eulerAngles.z);
            }
        }
    }
    //자전
    void ChildRotation(float direction)
    {
         planet.transform.Rotate(0, selfRotateSpeed * direction * Time.deltaTime,0);
    }

    protected override void AutoRotation()
    {
        if (isRotationZ)
        {
            if (!handle.isCatch)
            {
                if (!isLock)
                {
                    transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(transform.localRotation.eulerAngles.x,
                       transform.localRotation.eulerAngles.y, stopAngle[myPoint]), returnSpeed * Time.deltaTime);
                    if ((int)transform.localRotation.eulerAngles.z == stopAngle[myPoint])
                    {
                        isLock = true;
                    }
                }
            }
        }
        else
        {
            if (!handle.isCatch)
            {
                if (!isLock)
                {
                    transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(transform.localRotation.eulerAngles.x,
                       stopAngle[myPoint], transform.localRotation.eulerAngles.z), returnSpeed * Time.deltaTime);
                    if ((int)transform.localRotation.eulerAngles.y == stopAngle[myPoint])
                    {
                        isLock = true;
                    }
                }
            }
        }
    }

    public void GetPlanet(PlanetLine other)
    {
        if(transform.childCount == 0)
        {
            for(int i =0; i < other.overlapLine.Length; i++)
            {
                if(other.overlapLine[i] == other.myPoint)
                {
                    myPoint = overlapLine[i];
                    transform.localRotation = Quaternion.Euler(0,0, stopAngle[overlapLine[i]]);
                    other.isPlanet = false;
                    isPlanet = true;
                    planet.transform.parent.transform.SetParent(this.transform);
                }
            }
        }
    }
}
