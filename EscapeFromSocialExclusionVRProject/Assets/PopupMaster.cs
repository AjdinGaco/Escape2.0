using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PopupMaster : MonoBehaviour
{
    public GameObject popupPrefab;
    public GameObject popupObj;

    public Vector3 offset; // The offset from the target object
    public float rotationSpeed = 2.0f; // The speed of rotation

    public void PopUpImage(Sprite sprite)
    {
        if (sprite != null)
        {
            if (popupObj != null)
                Destroy(popupObj);
            popupObj = Instantiate(popupPrefab);
            popupObj.GetComponent<PopUpController>().Sprite = sprite;
            popupObj.transform.parent = this.gameObject.transform;
            popupObj.transform.localPosition = Vector3.zero;
            popupObj.transform.localRotation = Quaternion.identity;
            popupObj.transform.localScale = Vector3.one;
        }
    }
}
