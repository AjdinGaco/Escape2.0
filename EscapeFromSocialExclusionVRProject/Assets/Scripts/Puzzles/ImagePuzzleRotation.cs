using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagePuzzleRotation : RoomPuzzle
{
    public int rows = 3; // Number of rows in the puzzle
    public int columns = 3; // Number of columns in the puzzle
    public float padding = 10f; // Padding between puzzle pieces
    public Sprite image; // Image to use for the puzzle
    public AudioClip imageTurningSound;

    //temp fix, can't seem to make it work :(
    private float scaleSize = 1;

    private float pieceWidth; // Width of each puzzle piece
    private float pieceHeight; // Height of each puzzle piece

    private List<GameObject> puzzlepieces = new List<GameObject>();

    private void Start()
    {
        scaleSize = this.gameObject.transform.localScale.y;
        this.gameObject.transform.localScale = new Vector3(1, 1, 1);

        // Calculate the width and height of each puzzle piece based on the image size and puzzle dimensions
        pieceWidth = (image.texture.width - padding * (columns - 1)) / (float)columns;
        pieceHeight = (image.texture.height - padding * (rows - 1)) / (float)rows;

        // Calculate the size of the puzzle based on the number of rows, columns, and piece size
        float puzzleWidth = columns * pieceWidth + (columns - 1) * padding;
        float puzzleHeight = rows * pieceHeight + (rows - 1) * padding;

        // Set the size of the parent object to fit the puzzle
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(puzzleWidth, puzzleHeight);

        // Generate the puzzle pieces
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                // Create a new puzzle piece object
                GameObject puzzlePieceObj = new GameObject("PuzzlePiece (" + row + ", " + column + ")");
                puzzlepieces.Add(puzzlePieceObj);
                puzzlePieceObj.transform.SetParent(transform);
                puzzlePieceObj.AddComponent<ButtonScript>();
                puzzlePieceObj.GetComponent<ButtonScript>().roomPuzzle = this;
                puzzlePieceObj.GetComponent<ButtonScript>().interactionSound = imageTurningSound;
                puzzlePieceObj.AddComponent<BoxCollider>();
                puzzlePieceObj.GetComponent<BoxCollider>().size = new Vector3(110,110,10);
                puzzlePieceObj.tag = "Interactable";

                // Add an Image component to the puzzle piece object
                Image puzzlePieceImage = puzzlePieceObj.AddComponent<Image>();

                // Set the sprite of the Image component to the corresponding puzzle piece of the image
                Rect rect = new Rect(column * (pieceWidth + padding), row * (pieceHeight + padding), pieceWidth, pieceHeight);
                puzzlePieceImage.sprite = Sprite.Create(image.texture, rect, new Vector2(0.5f, 0.5f));

                // Set the RectTransform of the puzzle piece object
                RectTransform puzzlePieceTransform = puzzlePieceObj.GetComponent<RectTransform>();
                puzzlePieceTransform.pivot = new Vector2(0.5f, 0.5f);
                puzzlePieceTransform.anchorMin = new Vector2(0f, 1f);
                puzzlePieceTransform.anchorMax = new Vector2(0f, 1f);
                puzzlePieceTransform.sizeDelta = new Vector2(pieceWidth, pieceHeight);

                // Update the anchoredPosition based on the column and row
                float xPos = (columns - column - 1) * (pieceWidth + padding);
                float yPos = row * (pieceHeight + padding);
                puzzlePieceTransform.anchoredPosition = new Vector2(xPos, yPos);
            }
        }

        this.gameObject.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
        ScrambleRotationImages();

    }

    private void ScrambleRotationImages()
    {
        // Randomly rotate each puzzle piece by 90, 180, or 270 degrees
        foreach (GameObject child in puzzlepieces)
        {
            int randomRotation = Random.Range(0, 4) * 90;
            child.transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);
        }
    }

    public override void Clicked()
    {
        foreach (GameObject piece in puzzlepieces)
        {
            if (piece.GetComponent<ButtonScript>().clickstatus)
            {
                piece.GetComponent<ButtonScript>().clickstatus = false;
                TurnPiece(piece.transform);
            }
        }
    }


    public int completedPieces;
    public float turnDuration = 0.5f; // Set the duration of the turning animation in seconds
    public float turnCooldown = 0.5f; // Set the cooldown period in seconds

    private bool canTurn = true; // Flag to indicate whether a puzzle piece can be turned

    public void TurnPiece(Transform piece)
    {
        ConditionCheck();
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
    private void ConditionCheck()
    {
        completedPieces = 0;
        foreach (GameObject piece in puzzlepieces)
        {
            // Check if the puzzle piece is correctly oriented
            if (Mathf.Abs(piece.transform.localRotation.eulerAngles.z) < 0.001f)
            {
                completedPieces++;
                if (completedPieces >= puzzlepieces.Count)
                {
                    PuzzleDone();
                    foreach (GameObject gameObject in puzzlepieces)
                    {
                        gameObject.GetComponent<Collider>().enabled = false;
                    }
                }
            }
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
    }

    private IEnumerator TurnCooldown()
    {
        yield return new WaitForSeconds(turnCooldown);
        canTurn = true;
        ConditionCheck();
    }
}
