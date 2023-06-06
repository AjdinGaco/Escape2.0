using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BrailePuzzle : RoomPuzzle
{
    public GameObject centralBraileText;
    public List<GameObject> choices;
    public BlindnessManager blindnessManager;
    public GameObject poster0 ,poster1, poster2, poster3;

    public AudioClip audioCorrect, audioWrong;
    public List<String> incorrectChoices;
    public string rightchoice;

    //I'l do this puzzle in stages going tro them in order...
    public int stage = 0;
    private string correctChoice;

    void Start()
    {
        foreach(GameObject choice in choices)
        {
            choice.transform.LookAt(roomDirector.playerObj.transform);
        }
        rightchoice = "Glaucoma";
        centralBraileText.GetComponent<TextMeshProUGUI>().text = rightchoice;
        ShuffleChoices(rightchoice);
    }
    public void ShuffleChoices(string rightchoice)
    {
        StartCoroutine(ShuffleAnimation(rightchoice));
    }
    private IEnumerator ShuffleAnimation(string rightchoice)
    {
        // Fade Out
        float fadeDuration = 0.5f;
        float elapsedTime = 0f;
        Color originalColor = centralBraileText.GetComponent<TextMeshProUGUI>().color;
        Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(originalColor.a, 0f, elapsedTime / fadeDuration);
            centralBraileText.GetComponent<TextMeshProUGUI>().color = new Color(transparentColor.r, transparentColor.g, transparentColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Shuffle Choices
        foreach (GameObject choice in choices)
        {
            choice.GetComponent<TextMeshProUGUI>().text = incorrectChoices[Random.Range(0, incorrectChoices.Count)];
        }
        choices[Random.Range(0, choices.Count)].GetComponent<TextMeshProUGUI>().text = rightchoice;

        // Fade In
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, originalColor.a, elapsedTime / fadeDuration);
            centralBraileText.GetComponent<TextMeshProUGUI>().color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    public override void Clicked()
    {
        foreach(GameObject choice in choices)
        {
            if (choice.GetComponent<ButtonScript>().clickstatus)
            {
                choice.GetComponent<ButtonScript>().clickstatus = false;
                if (choice.GetComponent<TextMeshProUGUI>().text == rightchoice)
                {
                    
                    AudioSource.PlayClipAtPoint(audioCorrect, transform.position);
                    switch (stage)
                    {
                        case (0):
                            blindnessManager.ChangeVisionDisabilities(VisionDisabilities.Glaucoma);
                            rightchoice = "DiabeticRetinopathy";
                            poster0.gameObject.SetActive(false);
                            poster1.gameObject.SetActive(true);

                            break;
                        case (1):
                            blindnessManager.ChangeVisionDisabilities(VisionDisabilities.DiabeticRetinopathy);
                            poster1.gameObject.SetActive(false);
                            poster2.gameObject.SetActive(true);
                            rightchoice = "Cataracts";
                            break;
                        case (2):
                            blindnessManager.ChangeVisionDisabilities(VisionDisabilities.Cataracts);
                            poster2.gameObject.SetActive(false);
                            poster3.gameObject.SetActive(true);
                            rightchoice = "MacularDegeneration";
                            break;
                        case (3):
                            blindnessManager.ChangeVisionDisabilities(VisionDisabilities.MacularDegeneration);
                            this.gameObject.SetActive(false);
                            PuzzleDone();
                            break;
                        default:
                            blindnessManager.ChangeVisionDisabilities(VisionDisabilities.None);
                            PuzzleDone();
                            break;

                    }
                    stage++;
                    centralBraileText.GetComponent<TextMeshProUGUI>().text = rightchoice;
                    ShuffleChoices(rightchoice);
                }
                else
                {
                    ShuffleChoices(rightchoice);
                    AudioSource.PlayClipAtPoint(audioWrong, transform.position);
                }
            }
        }
    }
}
