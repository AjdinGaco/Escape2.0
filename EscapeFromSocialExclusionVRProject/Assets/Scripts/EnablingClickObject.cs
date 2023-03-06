using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablingClickObject : ObjectController
{
    public GameObject GameObject;
    public override void OnPointerClick()
    {
        GameObject.active = true;
    }
}
