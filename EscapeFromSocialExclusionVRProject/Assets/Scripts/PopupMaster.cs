using UnityEngine;

public class PopupMaster : MonoBehaviour
{
    public GameObject popupPrefab;
    public GameObject popupObj;

    public Vector3 offset; // The offset from the camera
    public float rotationSpeed = 2.0f; // The speed of rotation

    public GameObject mainCamera;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main.gameObject;
    }

    public void PopUpImage(Sprite sprite)
    {
        if (sprite != null)
        {
            if (popupObj != null)
                Destroy(popupObj);

            popupObj = Instantiate(popupPrefab, this.transform);
            popupObj.GetComponent<PopUpController>().Sprite = sprite;
            popupObj.transform.position = mainCamera.transform.position + mainCamera.transform.forward * offset.z;
            popupObj.transform.LookAt(mainCamera.transform);
        }
    }

    private void LateUpdate()
    {
        if (popupObj != null)
        {
            

            // Calculate the direction from the camera to the popup object
            Vector3 toPopup = popupObj.transform.position - mainCamera.transform.position;

            // Calculate the angle between the camera forward direction and the direction to the popup object
            float angle = Vector3.Angle(toPopup, mainCamera.transform.forward);

            if (angle > 90.0f)
            {
                // If the popup object is behind the camera, move it in front
                popupObj.transform.position = mainCamera.transform.position + mainCamera.transform.forward * offset.z;
                popupObj.transform.LookAt(mainCamera.transform);
            }
            else
            {
/*                // Otherwise, update the position of the popup object to follow the camera
                popupObj.transform.position = Vector3.Lerp(popupObj.transform.position, mainCamera.transform.position + mainCamera.transform.forward * offset.z, Time.deltaTime * rotationSpeed);*/
            }
        }
    }
}
