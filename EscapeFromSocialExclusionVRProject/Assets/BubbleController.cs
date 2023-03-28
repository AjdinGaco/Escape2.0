using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BubbleController : InteractionObj
{
    public int SceneNum;
    public Transform playerCam;
    private void Start()
    {
        this.gameObject.transform.LookAt(playerCam);
    }
    public override void ClickFunction()
    {
        SceneManager.LoadScene(SceneNum);
    }
}
