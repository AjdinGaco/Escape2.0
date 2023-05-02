using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoSceneOnInteraction : InteractionObj
{
    public int sceneNum;
    public UiManager uiManager;
    public override void ClickFunction()
    {
        StartCoroutine(uiManager.StartTransition(sceneNum));
    }
}
