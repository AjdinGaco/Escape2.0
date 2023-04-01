using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpGate : RoomEvent
{
    public Sprite gatedSprite;
    public override void OnStart()
    {
        roomDirector.PopupMaster.PopUpImage(gatedSprite);
    }
    public override void OnUpdate()
    {
        if (roomDirector.PopupMaster.popupObj == null)
        {
            isConditionMet = true;
        }
    }
}
