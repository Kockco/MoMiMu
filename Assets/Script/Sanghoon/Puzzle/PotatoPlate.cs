using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoPlate : Plate
{
    [Header("인접한 감자의 개수와 번호")]
    [SerializeField]
    int[] potatoNumber;

    GameObject potatoBasket;
    GameObject[] potato;
    
    protected override void Start()
    {
        base.Start();

        potato = GameObject.FindGameObjectsWithTag("Potato");
        potatoBasket = GameObject.Find("PotatoBasket");
    }

    //자동으로 움직이기
    protected override void AutoRotation()
    {
        if (!handle.isCatch)
        {
            if (!isLock)
            {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(transform.localRotation.eulerAngles.x,
            stopAngle[myPoint], transform.localRotation.eulerAngles.z), returnSpeed * Time.deltaTime);
                if ((int)transform.localRotation.eulerAngles.y == stopAngle[myPoint])
                {
                    SetPotatoParent();
                    isLock = true;
                }
            }
        }
    }

    public void GetPotatoChild()
    {
        foreach (GameObject pot in potato)
        {
            foreach (int num in potatoNumber)
            {
                if (pot.GetComponent<Potato>().myNum == num)
                {
                    pot.transform.parent = transform;
                }
            }
        }
    }

    public void SetPotatoParent()
    {
        for (int i = 1; i <= potatoNumber.Length; i++)
        {
            transform.GetChild(1).GetComponent<Potato>().PositionReset();
            transform.GetChild(1).SetParent(potatoBasket.transform);
        }
    }
}
