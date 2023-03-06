using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpButton : ObjectController
{
    public PopUpController PopUpController;
    public bool clickstatus = false;
    public override void OnPointerClick()
    {
        clickstatus = true;
        PopUpController.SendMessage("Clicked");
    }
}
