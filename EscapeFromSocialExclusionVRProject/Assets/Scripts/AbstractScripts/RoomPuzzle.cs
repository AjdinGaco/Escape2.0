using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPuzzle : MonoBehaviour
{
    public RoomDirector roomDirector;
    public bool completion = false;
    public AudioClip puzzleCompletionSound;
    public Light puzzleCompletionLight;

    public virtual void Clicked()
    {

    }

    public void PuzzleDone()
    {
        if (puzzleCompletionSound)
        {
            AudioSource.PlayClipAtPoint(puzzleCompletionSound, transform.position);
            if (puzzleCompletionLight)
                puzzleCompletionLight.color = Color.green;
        }

        completion = true;
		if (disableOnCompletion){
			// Disable the game object after 5 seconds
			Invoke("DisablePuzzleGameObject", 5f);
		}
        
    }
	public bool disableOnCompletion = true;
    private void DisablePuzzleGameObject()
    {
        gameObject.SetActive(false);
    }
}
