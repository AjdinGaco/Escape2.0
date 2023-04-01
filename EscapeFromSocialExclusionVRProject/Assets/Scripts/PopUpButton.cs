using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpButton : InteractionObj
{
    public PopUpController PopUpController;
    public bool clickstatus = false;
    public override void ClickFunction()
    {
        clickstatus = true;
        PopUpController.SendMessage("Clicked");
    }
}
