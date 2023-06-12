using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoSceneOnInteraction : InteractionObj
{
    public int sceneNum;
    public UiManager uiManager;
    //this will change the scene when clicked.
    public override void ClickFunction()
    {
        this.gameObject.tag = "Untagged";
        StartCoroutine(uiManager.StartTransition(sceneNum));
    }
}
