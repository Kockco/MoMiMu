using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour
{
    //[HideInInspector]
    public bool isCatch;

    public Plate plate;

    protected virtual void Start()
    {
        isCatch = false;
    }

    public virtual void CatchCheck() { return; }

    //핸들잡고 돌리는 부분 캐릭터에게
    public virtual void HandleRotate(float direction) { return; }
}
