using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEvent : RoomEvent
{
    /// <summary>
    /// A general event that will be used for enabling, disabled objs. Not much use after that.
    /// </summary>
    public override void OnStart()
    {
        isConditionMet = true;
    }
}

