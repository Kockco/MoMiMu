using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momi_Jump : MomiFSMState
{

    public override void BeginState()
    {
        base.BeginState();
        
        momiSound.JumpSoundMomi();
        // anime.SetInteger("Momi_Jump", 1);
        anime.SetTrigger("Momi_Jump");

        Invoke("EndJumpSound", 0.8f);
        Invoke("EndJump", 1.15f);
    }

    public override void EndState()
    {
        base.EndState();
        
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        // if (isGround) manager.SetState(MomiState.Idle);
    }

    void EndJump()
    {
        // anime.SetBool("Momi_Jump", false);
        manager.SetState(MomiState.Idle);
    }

    void EndJumpSound()
    {
        momiSound.WalkSoundMomi(momiSound.isState);
    }
}
