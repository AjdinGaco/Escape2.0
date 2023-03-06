using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : ObjectController
{
    public RoomPuzzle roomPuzzle;
    public bool clickstatus = false;
    public override void OnPointerClick()
    {
        clickstatus = true;
        roomPuzzle.SendMessage("Clicked");
    }
    public override void SetMaterial(bool gazedAt)
    {
        if (clickstatus)
            base.SetMaterial(true);
        else
            base.SetMaterial(gazedAt);
    }
}
