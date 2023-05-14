using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPuzzle : MonoBehaviour
{
    public RoomDirector roomDirector;
    public bool completion = false;
    public AudioClip puzzleCompletionSound;
    public virtual void Clicked()
    {

    }

    public void PuzzleDone()
    {
        if (puzzleCompletionSound)
             AudioSource.PlayClipAtPoint(puzzleCompletionSound, transform.position);
        completion = true;
    }
}
