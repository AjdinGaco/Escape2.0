using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    // This will be the way the game remembers what is happening.
    public bool Room1Completion, Room2Completion, Room3Completion;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
