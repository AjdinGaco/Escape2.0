using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablingClickObject : InteractionObj
{
    public GameObject GameObjectToEnable;
    public bool DestroyOnClick = false;
    public override void ClickFunction()
    {
        if (GameObjectToEnable.active)
            GameObjectToEnable.SetActive(false);
        else
            GameObjectToEnable.SetActive(true);


        if (DestroyOnClick)
            Destroy(gameObject);
    }
}
