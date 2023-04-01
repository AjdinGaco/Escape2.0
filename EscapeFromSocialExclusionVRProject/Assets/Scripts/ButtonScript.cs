using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : InteractionObj
{
    public RoomPuzzle roomPuzzle;
    public bool clickstatus = false;
    public override void ClickFunction()
    {
        clickstatus = true;
        roomPuzzle.SendMessage("Clicked");
    }
}
