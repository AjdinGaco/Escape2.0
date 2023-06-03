using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : InteractionObj
{
    public RoomPuzzle roomPuzzle;
    public bool clickstatus = false;
    private bool canClick = true;

    public override void ClickFunction()
    {
        if (canClick)
        {
            clickstatus = true;
            roomPuzzle.SendMessage("Clicked");
            StartCoroutine(DelayBeforeNextClick());
        }
    }

    private IEnumerator DelayBeforeNextClick()
    {
        canClick = false;
        float delayDuration = 0.5f; 
        yield return new WaitForSeconds(delayDuration);
        canClick = true;
    }
}