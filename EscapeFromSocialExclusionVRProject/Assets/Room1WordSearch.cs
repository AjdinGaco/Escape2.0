using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Room1WordSearch : RoomPuzzle
{
    public ButtonScript buttonAid, buttonHumanitarian, buttonAdvocacy, buttonRehabilitation, buttonDisability, buttonInclusion, buttonClose;
    public GameObject textAid, textHumanitarian, textAdvocacy, textRehabilitation, textDisability, textInclusion;
    public Sprite spriteAid, spriteHumanitarian, spriteAdvocacy, spriteRehabilitation, spriteDisability, spriteInclusion;
    private bool doneAid, doneHumanitarian, doneAdvocacy, doneRehabilitation, doneDisability, doneInclusion;

    public override void Clicked()
    {
        if (buttonClose.clickstatus)
        {
            buttonClose.clickstatus = false;
            this.gameObject.SetActive(false);
        }
        else
        {
            if (buttonAid.clickstatus && !doneAid)
            {
                doneAid = true;
                roomDirector.PopupMaster.PopUpImage(spriteAid);
                textAid.SetActive(true);
            }


            if (buttonHumanitarian.clickstatus && !doneHumanitarian)
            {
                doneHumanitarian = true;
                roomDirector.PopupMaster.PopUpImage(spriteHumanitarian);
                textHumanitarian.SetActive(true);
            }


            if (buttonAdvocacy.clickstatus && !doneAdvocacy)
            {
                doneAdvocacy = true;
                roomDirector.PopupMaster.PopUpImage(spriteAdvocacy);
                textAdvocacy.SetActive(true);

            }


            if (buttonRehabilitation.clickstatus && !doneRehabilitation)
            {
                doneRehabilitation = true;
                roomDirector.PopupMaster.PopUpImage(spriteRehabilitation);
                textRehabilitation.SetActive(true);
            }


            if (buttonDisability.clickstatus && !doneDisability )
            {
                doneDisability = true;
                roomDirector.PopupMaster.PopUpImage(spriteDisability);
                textDisability.SetActive(true);
            }


            if (buttonInclusion.clickstatus && !doneInclusion)
            {
                doneInclusion = true;
                roomDirector.PopupMaster.PopUpImage(spriteInclusion);
                textInclusion.SetActive(true);
            }


            if (buttonAid.clickstatus && buttonHumanitarian.clickstatus && buttonAdvocacy.clickstatus && buttonRehabilitation.clickstatus && buttonDisability.clickstatus && buttonInclusion.clickstatus)
                completion = true;
        }

    }
}
