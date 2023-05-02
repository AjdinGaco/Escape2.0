using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressUpPuzzle : RoomPuzzle
{
    //This isn't much of a puzzle then its a click minigame, however for the sake of code i'l consider it a puzzle. No one will notice it. Nor read this. 
    public ButtonScript closetDoor1, closetDoor2;
    public Animator animator;

    private bool closetOpenStatus = false;
    public override void Clicked()
    {
        //Check which door was pressed, however it doesn't matter for now
        if (closetDoor1.clickstatus)
        {
            closetDoor1.clickstatus = false;
        }
        if (closetDoor2.clickstatus)
        {
            closetDoor2.clickstatus = false;
        }

        if (closetOpenStatus)
        {
            animator.SetBool("OpenStatus", closetOpenStatus);
            closetOpenStatus = false;
        }
        else
        {
            animator.SetBool("OpenStatus", closetOpenStatus);
            closetOpenStatus = true;
        }
        completion = true;
    }
}
