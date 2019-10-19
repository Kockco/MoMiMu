using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momi_Move : MomiFSMState
{
    public float walkTimer;

    public override void BeginState()
    {
        base.BeginState();

        // rig = GetComponent<Rigidbody>();
        anime.SetBool("Momi_Move", true);
    }

    public override void EndState()
    {
        base.EndState();

        anime.SetBool("Momi_Move", false);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!Input.anyKey)
            manager.SetState(MomiState.Idle);
    }

    void OnCollisionStay(Collision col)
    {
        switch (col.transform.tag)
        {
            case "Snow":
                momiSound.isState = true;
                break;
            case "Stone":
                momiSound.isState = false;
                break;
        }
    }
}
/*
    void CameraFrontViewMomi()
    {
        Quaternion tempQuat = cameraRot.transform.rotation;
        tempQuat.x = tempQuat.z = 0;

        Quaternion charQuat = Quaternion.Lerp(transform.rotation, cameraRot.rotation, Time.deltaTime * 10);
        charQuat.x = charQuat.z = 0;

        transform.rotation = charQuat;
    }
*/
