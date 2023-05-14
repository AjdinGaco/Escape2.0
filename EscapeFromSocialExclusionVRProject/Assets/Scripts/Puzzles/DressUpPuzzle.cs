using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DressUpPuzzle : RoomPuzzle
{
    //This isn't much of a puzzle then its a click minigame, however for the sake of code i'l consider it a puzzle. No one will notice it. Nor read this. 
    public ButtonScript closetDoor1, closetDoor2;
    public AudioClip closetOpenning, closetClosing;

    public ButtonScript ThumpbsUp, ThumpbsDown;
    public Animator animator;
    public GameObject ToEnableWhenOpened;

    public AudioClip audioCorrect, audioWrong;

    public SpriteRenderer ShowImage;
    public List<Sprite> trueImages = new List<Sprite>();
    public List<Sprite> falseImages = new List<Sprite>();
    int currentIndex = 0;
    bool[] imageIsTrue;
    private List<Sprite> combinedList = new List<Sprite>();
    System.Random rand = new System.Random();

    private bool closetOpenStatus = false;

    public void Start()
    {
        // Combine the two image lists
        combinedList.AddRange(trueImages);
        combinedList.AddRange(falseImages);

        // Create an array to keep track of which images are accessible
        imageIsTrue = new bool[combinedList.Count];
        for (int i = 0; i < trueImages.Count; i++)
        {
            imageIsTrue[i] = true;
        }
        ShowImage.sprite = combinedList[currentIndex];
    }
    public override void Clicked()
    {
        Debug.Log("Clicked");
        //Check which door was pressed, however it doesn't matter for now
        if (closetDoor1.clickstatus || closetDoor2.clickstatus)
        {
            closetDoor1.clickstatus = false;
            closetDoor2.clickstatus = false;
            if (closetOpenStatus)
            {
                animator.SetBool("OpenStatus", false);
                ToEnableWhenOpened.SetActive(false);
                closetOpenStatus = false;
                AudioSource.PlayClipAtPoint(closetClosing, transform.position);
            }
            else
            {
                animator.SetBool("OpenStatus", true);
                ToEnableWhenOpened.SetActive(true);
                closetOpenStatus = true;
                AudioSource.PlayClipAtPoint(closetOpenning, transform.position);
            }
        }


        if (ThumpbsUp.clickstatus)
        {
            ThumpbsUp.clickstatus = false;
            // Check if the player guessed correctly
            if (imageIsTrue[currentIndex])
            {
                // Play the correct sound
                AudioSource.PlayClipAtPoint(audioCorrect, transform.position);

                // Show the next image
                currentIndex++;
                if (currentIndex < combinedList.Count-1)
                {
                    ShowImage.sprite = combinedList[currentIndex];
                }
                else
                {
                    // All images have been shown, close the closet
                    closetOpenStatus = false;
                    animator.SetBool("OpenStatus", false);
                    ToEnableWhenOpened.SetActive(false);
                    PuzzleDone();
                    AudioSource.PlayClipAtPoint(closetClosing, transform.position);
                    closetDoor1.tag = "Untagged";
                    closetDoor2.tag = "Untagged";
                }
            }
            else
            {
                // Play the wrong sound
                AudioSource.PlayClipAtPoint(audioWrong, transform.position);
            }
        }
        if (ThumpbsDown.clickstatus)
        {
            ThumpbsDown.clickstatus = false;
            // Check if the player guessed correctly
            if (!imageIsTrue[currentIndex])
            {
                // Play the correct sound
                AudioSource.PlayClipAtPoint(audioCorrect, transform.position);

                // Show the next image
                currentIndex++;
                if (currentIndex < combinedList.Count-1)
                {
                    ShowImage.sprite = combinedList[currentIndex];
                }
                else
                {
                    // All images have been shown, close the closet
                    closetOpenStatus = false;
                    animator.SetBool("OpenStatus", false);
                    ToEnableWhenOpened.SetActive(false);
                    PuzzleDone();
                    AudioSource.PlayClipAtPoint(closetClosing, transform.position);
                    closetDoor1.tag = "Untagged";
                    closetDoor2.tag = "Untagged";
                }
            }
            else
            {
                // Play the wrong sound
                AudioSource.PlayClipAtPoint(audioWrong, transform.position);
            }
        }
    }
}
