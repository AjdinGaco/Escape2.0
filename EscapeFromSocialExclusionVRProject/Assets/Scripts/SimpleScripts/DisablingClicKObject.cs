using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablingClicKObject : InteractionObj
{
    public GameObject GameObjectToDisable;
    public bool DestroyOnClick = false;
    public override void ClickFunction()
    {
        GameObjectToDisable.SetActive(false);
        if (DestroyOnClick)
            Destroy(gameObject);
    }
}
