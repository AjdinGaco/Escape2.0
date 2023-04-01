using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObjOnStart : MonoBehaviour
{
    public GameObject ObjToLookAt;
    void Start()
    {
        this.transform.LookAt(ObjToLookAt.transform);
    }
}
