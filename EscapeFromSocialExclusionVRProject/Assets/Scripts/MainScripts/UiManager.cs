using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class UiManager : MonoBehaviour
{
    public Image transitionImage; // assign the black image object to this variable in the Inspector
    public float transitionDuration = 1f; // set the duration of the transition in seconds
    public float waitDuration = 1f; // set the duration to wait after the transition is complete in seconds
    public IEnumerator StartTransition(int sceneNum)
    {
        // gradually increase the alpha value of the image
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;

            Color color = transitionImage.color;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / transitionDuration);
            transitionImage.color = color;

            yield return null;
        }

        // wait for the specified duration after the transition is complete
        yield return new WaitForSeconds(waitDuration);

        // load the next scene
        SceneManager.LoadScene(sceneNum);
    }

    private void Start()
    {
        Color color = transitionImage.color;
        color.a = 1f;
        transitionImage.color = color;
        StartCoroutine(StartFadeInTransition());
    }
    public IEnumerator StartFadeInTransition()
    {
        // gradually decrease the alpha value of the image
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;

            Color color = transitionImage.color;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / transitionDuration);
            transitionImage.color = color;

            yield return null;
        }
    }
}


