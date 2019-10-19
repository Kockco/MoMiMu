using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public int PuzzleNumber;

    //핸들,각도,속도
    #region
    [Header("연결될 핸들")]
    [SerializeField]
    protected Handle handle;

    [Header("각도의 갯수")]
    public int cutAngle;

    [Header("나의 지점")]
    public int myPoint;

    [Header("회전속도")]
    [SerializeField]
    protected float rotateSpeed;

    [Header("제자리로 오는 속도")]
    [SerializeField]
    protected float returnSpeed;
    #endregion

    #region
    //멈춰야되는 각, 중간 각, 움직이는중인지, 감자, 어느각으로 도는지?
    [HideInInspector]
    public float[] stopAngle;

    [HideInInspector]
    public float[] centerAngle;
    
    public bool isLock;
    #endregion

    protected virtual void Start()
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

        //시작할때 지점
        Quaternion startingAngleY = Quaternion.Euler(0, stopAngle[myPoint],0);
        transform.parent.localRotation = startingAngleY;
    }

    protected virtual void Update()
    {
        AutoRotation();
        AngleLimit();
    }

    //왼쪽으로 맟출건지 오른쪽으로 맟출건지 결정
    public virtual void AngleCheck()
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

    //최대값 최소값 지정
    protected virtual void AngleLimit()
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

    protected virtual void AutoRotation()
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

    //회전
    public virtual void Rotate(float direction)
    {
        transform.Rotate(0, rotateSpeed * direction * Time.deltaTime,0);
    }
}
