using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class WordPuzzleSearch : RoomPuzzle
{
    public GameObject textPrefab;
    public float spawnRadius = 100f;

    public string[] importantWords;
    public string[] unimportantWords;

    private List<string> allWords = new List<string>();
    private List<Vector2> spawnedWordPositions = new List<Vector2>();
    private List<GameObject> unimportantWordsObj = new List<GameObject>();
    private List<GameObject> importantWordsObj = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        allWords.AddRange(importantWords);
        allWords.AddRange(unimportantWords);

        Vector3 parentPos = transform.position; // get parent object position

        foreach (string word in allWords)
        {
            Vector2 randomPos;
            do
            {
                // generate random position relative to parent object
                randomPos = new Vector2(Random.Range(parentPos.x - spawnRadius, parentPos.x + spawnRadius),
                                        Random.Range(parentPos.z - spawnRadius, parentPos.z + spawnRadius));
            } while (spawnedWordPositions.Any(pos => Vector2.Distance(randomPos, pos) < textPrefab.GetComponent<TextMeshProUGUI>().preferredWidth));
            spawnedWordPositions.Add(randomPos);

            GameObject newTextObject = Instantiate(textPrefab, transform);
            newTextObject.transform.localPosition = randomPos;
            newTextObject.GetComponent<TextMeshProUGUI>().text = word;
            newTextObject.name = word + "_Obj";

            //If word is important add the colider
            if (importantWords.Contains(word))
            {
                newTextObject.AddComponent<ButtonScript>();
                newTextObject.GetComponent<ButtonScript>().roomPuzzle = this;
                newTextObject.GetComponent<ButtonScript>().gazeDurationOverride = 0.5f;
                newTextObject.AddComponent<BoxCollider>();
                newTextObject.GetComponent<BoxCollider>().size = new Vector3(20, 10, 10);
                newTextObject.tag = "Interactable";
                importantWordsObj.Add(newTextObject);
            }
            else
            {
                unimportantWordsObj.Add(newTextObject);
            }
        }
        FacePlayer();
    }

    public override void Clicked()
    {
        for (int i = importantWordsObj.Count - 1; i >= 0; i--)
        {
            GameObject piece = importantWordsObj[i];

            if (piece.GetComponent<ButtonScript>().clickstatus)
            {
                TextMeshProUGUI textMesh = piece.GetComponent<TextMeshProUGUI>();
                Material material = textMesh.fontMaterial;
                material.SetColor("_UnderlayColor", Color.yellow);
                // Assign the modified Material back to the TextMeshPro component
                textMesh.fontMaterial = material;

                // Remove candidate from list
                piece.GetComponent<BoxCollider>().enabled = false;
                importantWordsObj.RemoveAt(i);
            }
        }

        if (importantWordsObj.Count == 0)
        {
            completion = true;
            StartCoroutine(FadeOutNonImportantWords());
        }
    }
    IEnumerator FadeOutNonImportantWords()
    {
        List<GameObject> nonImportantWordsObj = new List<GameObject>();
        foreach (GameObject wordObj in unimportantWordsObj)
        {
            if (!importantWordsObj.Contains(wordObj))
            {
                nonImportantWordsObj.Add(wordObj);
            }
        }

        float fadeDuration = 3f; // Duration of fade-out in seconds
        float fadeStartTime = Time.time;
        while (Time.time < fadeStartTime + fadeDuration)
        {
            float timePassed = Time.time - fadeStartTime;
            float alpha = 1f - timePassed / fadeDuration;

            foreach (GameObject wordObj in nonImportantWordsObj)
            {
                TextMeshProUGUI textMesh = wordObj.GetComponent<TextMeshProUGUI>();
                Color color = textMesh.color;
                color.a = alpha;
                textMesh.color = color;
            }

            yield return null;
        }

        // Remove non-important words from scene and list
        foreach (GameObject wordObj in nonImportantWordsObj)
        {
            unimportantWordsObj.Remove(wordObj);
            Destroy(wordObj);
        }
    }

    public void FacePlayer()
    {
        gameObject.transform.transform.LookAt(roomDirector.playerObj.transform);
        foreach (GameObject obj in importantWordsObj)
        {
            obj.transform.LookAt(roomDirector.playerObj.transform);
        }
        foreach (GameObject obj in unimportantWordsObj)
        {
            obj.transform.LookAt(roomDirector.playerObj.transform);
        }
    }
}
