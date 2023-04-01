using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGate : RoomEvent
{
    public RoomPuzzle roomPuzzle;
    public override void OnStart()
    {
    }
    public override void OnUpdate()
    {
        if (roomPuzzle.completion)
            isConditionMet = true;
    }
}
