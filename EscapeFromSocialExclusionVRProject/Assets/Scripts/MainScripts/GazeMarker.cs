using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeMarker : InteractionObj
{
    public GameObject playerObj;
    private Collider _collider;

    public Animator animator;
    public int animPos;
    public UiManager uiManager;
    public void Start()
    {
        _collider = gameObject.GetComponent<Collider>();
    }
    public override void ClickFunction()
    {
        StartCoroutine(TeleportCoroutine());
    }
    private IEnumerator TeleportCoroutine()
    {
        yield return StartCoroutine(uiManager.StartTeleportTransitionIn());

        //Teleport player to itself
        playerObj.transform.position = gameObject.transform.position;
        if (animator != null)
            animator.SetInteger("Pos", animPos);

        yield return StartCoroutine(uiManager.StartTeleportTransitionOut());
    }
}
