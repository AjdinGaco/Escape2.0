using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationGate : RoomEvent
{
    public Animator animator;
    public int PosToSet = 1;
    public override void OnStart()
    {
        isConditionMet = true;
        animator.SetInteger("Pos", PosToSet);
    }
}
