using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInteraction : MonoBehaviour
{
    public ImagePuzzle roomPuzzle;
    void Start()
    {
    }
    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnter()
    {
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExit()
    {
    }

    /// <summary>
    /// This method is called by the Main Camera when it is gazing at this GameObject and the screen
    /// is touched.
    /// </summary>
    public void OnPointerClick()
    {
        ClickFunction();
    }

    public virtual void ClickFunction()
    {
        Transform clickedPiece = transform;
        roomPuzzle.TurnPiece(clickedPiece);
    }
}
