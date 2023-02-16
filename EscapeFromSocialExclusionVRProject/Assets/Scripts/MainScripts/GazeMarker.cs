using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeMarker : InteractionObj
{
    public GameObject playerObj;
    private Collider _collider;
    public void Start()
    {
        _collider = gameObject.GetComponent<Collider>();
    }
    public override void ClickFunction()
    {
        //Teleport player to itself
        playerObj.transform.position = gameObject.transform.position;
    }
}
