using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubDoor : InteractionObj 
{
    public int SceneNum; 
    public override void ClickFunction()
    {
        SceneManager.LoadScene(SceneNum);
    }
}
