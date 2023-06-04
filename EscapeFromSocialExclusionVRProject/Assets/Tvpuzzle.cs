using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tvpuzzle : RoomPuzzle
{
    public GameObject RemoteObj;
    public Sprite ObjectToFind1Sprite, ObjectToFind2Sprite, ObjectToFind3Sprite;
    public Sprite Ishihara1, Ishihara2, Ishihara3, Ishihara4;
    public GameObject ObjectToFind1, ObjectToFind2, ObjectToFind3;
    private GameObject CurrentObjToFind;
    public Image ScreenImage;
    public AudioClip foundObjectSound;
    public BlindnessManager BlindnessManager;

    public int stage = 0;

    public override void Clicked()
    {
        if (RemoteObj.GetComponent<ButtonScript>().clickstatus == true)
        {
            if (stage != 3)
            {
                //Start the puzzle
                ScreenImage.gameObject.SetActive(true);
                RemoteObj.gameObject.tag = "Untagged";
                RemoteObj.GetComponent<ButtonScript>().clickstatus = false;
                CurrentObjToFind = ObjectToFind1;
                NextObj(CurrentObjToFind);
                ScreenImage.GetComponent<Image>().sprite = ObjectToFind1Sprite;
                BlindnessManager.ChangeVisionDisabilities(VisionDisabilities.Glaucoma);
            }
            else
            {
                //This is the 3rd puzzle images
                // Start the Ishihara puzzle
                StartCoroutine(StartIshiharaPuzzle());
            }
            
        }
        if (stage != 3)
        {
            if (CurrentObjToFind.GetComponent<ButtonScript>().clickstatus == true)
            {
                CurrentObjToFind.GetComponent<ButtonScript>().clickstatus = false;
                AudioSource.PlayClipAtPoint(foundObjectSound, transform.position);
                switch (stage)
                {
                    case (0):
                        BlindnessManager.ChangeVisionDisabilities(VisionDisabilities.Cataracts);
                        CurrentObjToFind = ObjectToFind2;
                        NextObj(CurrentObjToFind);
                        ScreenImage.GetComponent<Image>().sprite = ObjectToFind2Sprite;
                        stage = 1;
                        break;
                    case (1):
                        BlindnessManager.ChangeVisionDisabilities(VisionDisabilities.MacularDegeneration);
                        CurrentObjToFind = ObjectToFind3;
                        NextObj(CurrentObjToFind);
                        ScreenImage.GetComponent<Image>().sprite = ObjectToFind3Sprite;
                        stage = 2;
                        break;
                    case (2):
                        completion = true;
                        CurrentObjToFind = null;
                        RemoteObj.gameObject.tag = "Interactable";
                        ScreenImage.gameObject.SetActive(false);
                        NextObj(null);
                        stage = 3;
                        PuzzleDone();
                        break;
                }
            }
        }
        

    }
    private GameObject prevObj;
    private void NextObj(GameObject newobj)
    {
        if (prevObj)
        {
            prevObj.tag = "Untagged";
        }
        newobj.tag = "Interactable";
        newobj.AddComponent<ButtonScript>();
        newobj.GetComponent<ButtonScript>().roomPuzzle = this;
        prevObj = newobj;
    }

    // Variables for Ishihara puzzle
    private bool isIshiharaPuzzle = false;
    private float ishiaharaImageDelay = 2f;
    private Sprite[] ishiaharaImages;
    private int currentIshiharaIndex;
    private IEnumerator StartIshiharaPuzzle()
    {
        isIshiharaPuzzle = true;
        ScreenImage.gameObject.SetActive(true);
        BlindnessManager.ChangeVisionDisabilities(VisionDisabilities.None);
        RemoteObj.gameObject.tag = "Untagged";
        RemoteObj.GetComponent<ButtonScript>().clickstatus = false;

        ishiaharaImages = new Sprite[] { Ishihara1, Ishihara2, Ishihara3, Ishihara4 };
        currentIshiharaIndex = 0;

        while (currentIshiharaIndex < ishiaharaImages.Length)
        {
            ScreenImage.GetComponent<Image>().sprite = ishiaharaImages[currentIshiharaIndex];
            yield return new WaitForSeconds(ishiaharaImageDelay);
            currentIshiharaIndex++;
        }

        RemoteObj.gameObject.tag = "Interactable";
        ScreenImage.gameObject.SetActive(false);
    }

}
