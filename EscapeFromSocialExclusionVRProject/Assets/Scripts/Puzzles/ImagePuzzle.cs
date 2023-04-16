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
            child.GetComponent<ButtonScript>().roomPuzzle = this;
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
    public float turnDuration = 0.5f; // Set the duration of the turning animation in seconds
    public float turnCooldown = 0.5f; // Set the cooldown period in seconds

    private bool canTurn = true; // Flag to indicate whether a puzzle piece can be turned

    public void TurnPiece(Transform piece)
    {
        if (canTurn)
        {
            // Turn the puzzle piece by 90 degrees around the z-axis
            Quaternion targetRotation = piece.rotation * Quaternion.Euler(0f, 0f, 90f);
            StartCoroutine(TurnAnimation(piece, targetRotation));

            // Set the flag to prevent turning for the cooldown period
            canTurn = false;

            // Start the cooldown coroutine
            StartCoroutine(TurnCooldown());
        }
    }

    private IEnumerator TurnAnimation(Transform piece, Quaternion targetRotation)
    {
        float elapsed = 0f;
        Quaternion initialRotation = piece.rotation;

        while (elapsed < turnDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / turnDuration);
            piece.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);
            yield return null;
        }

        piece.rotation = targetRotation;

        // Check if the puzzle piece is correctly oriented
        if (Mathf.Abs(piece.localRotation.eulerAngles.z) < 0.001f)
        {
            completedPieces++;
            if (completedPieces >= puzzlepieces.Count)
            {
                completion = true;
                foreach (GameObject gameObject in puzzlepieces)
                {
                    gameObject.GetComponent<Collider>().enabled = false;
                }
            }
        }

        // Debug the rotation after turning
        Debug.Log("Rotation after turning: " + piece.localRotation.eulerAngles.z);
    }

    private IEnumerator TurnCooldown()
    {
        yield return new WaitForSeconds(turnCooldown);
        canTurn = true;
    }
}

