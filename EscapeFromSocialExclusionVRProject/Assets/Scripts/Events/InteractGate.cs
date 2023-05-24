using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractGate : RoomEvent
{
    public void InteractionDone()
    {
        isConditionMet = true;
    }
}
