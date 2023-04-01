using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroySpawnObj : MonoBehaviour
{
    public GameObject spawnObj;
    private void OnDestroy()
    {
        GameObject spawnedObj = Instantiate(spawnObj);
        spawnedObj.transform.position = gameObject.transform.position;
        spawnedObj.transform.rotation = gameObject.transform.rotation;
    }
}
