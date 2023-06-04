using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class KeyPadPuzzle : RoomPuzzle
{
    public List<GameObject> numberButtons;
    public TextMeshProUGUI visibleCode;
    public string correctCode;
    public AudioClip audioWrong;
    public string codeEntered = "";

    public override void Clicked()
    {
        foreach (GameObject button in numberButtons)
        {
            ButtonScript buttonScript = button.GetComponent<ButtonScript>();

            if (buttonScript.clickstatus)
            {
                buttonScript.clickstatus = false;
                codeEntered += button.name;
                break;
            }
        }

        ShowCode();

        if (codeEntered == correctCode)
        {
            completion = true;
            foreach(GameObject button in numberButtons)
            {
                button.tag = "Untagged";
            }
            PuzzleDone();
        }

        if (!completion && codeEntered.Length > 3)
        {
            codeEntered = "";
            AudioSource.PlayClipAtPoint(audioWrong, transform.position);
            ShowCode();
        }


    }

    private void ShowCode()
    {
        visibleCode.text = codeEntered;
    }
}
