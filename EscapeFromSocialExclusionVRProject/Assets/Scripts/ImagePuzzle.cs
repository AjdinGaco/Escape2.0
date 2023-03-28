using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ImagePuzzle : RoomPuzzle
{
    public List<GameObject> puzzlepieces;
    private void Start()
    {
        // Randomly rotate each puzzle piece by 90, 180, or 270 degrees
        foreach (GameObject child in puzzlepieces)
        {
            int randomRotation = Random.Range(0, 4) * 90;
            child.transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);
        }
    }

    //This is temp due to time preasure i copied it froom the other puzzle
    public ButtonScript buttonClose;
    public override void Clicked()
    {
        if (buttonClose.clickstatus)
        {
            buttonClose.clickstatus = false;
            this.gameObject.SetActive(false);
        }
    }
    public int completedPieces;
    public float turnCooldown = 0.5f; // Set the cooldown period in seconds

    private bool canTurn = true; // Flag to indicate whether a puzzle piece can be turned

    public void TurnPiece(Transform piece)
    {
        if (canTurn)
        {
            // Turn the puzzle piece by 90 degrees around the z-axis
            piece.Rotate(Vector3.forward, 90f);

            // Set the flag to prevent turning for the cooldown period
            canTurn = false;

            // Start the cooldown coroutine
            StartCoroutine(TurnCooldown());

            // Check if all puzzle pieces have been correctly oriented
            completedPieces = 0;
            foreach (GameObject gameObject in puzzlepieces)
            {
                if (gameObject.transform.localRotation.eulerAngles.z < 0.001f)
                {
                    completedPieces++;
                }
            }
            if (completedPieces >= puzzlepieces.Count)
            {
                completion = true;
                foreach (GameObject gameObject in puzzlepieces)
                {
                    gameObject.GetComponent<Collider>().enabled = false;
                }
            }
        }
    }

    private IEnumerator TurnCooldown()
    {
        yield return new WaitForSeconds(turnCooldown);
        canTurn = true;
    }
}

