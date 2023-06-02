using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float sensitivity = 2.0f;
    public GameObject CameraObj;
    private CharacterController characterController;
    private Vector3 movement;
    public CameraPointer CameraPointer;

    private int platform;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        platform = GetPlatform();


        //Incase of Cardboard 
        if (platform == 2)
        {
            characterController.enabled = false;
            CameraPointer.AutoClickEnabled = true;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        switch (platform)
        {
            case 1:
                // code specific to Windows PC platform
                PcPlayerUpdate();
                break;
            case 2:
                // code specific to Android platform
                break;
            case 5:
                // code specific to Oculus platform
                break;
            default:
                // code for other platforms
                break;
        }
    }

    public void PcPlayerUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement.Set(horizontal, 0, vertical);
        movement = movement.normalized * speed * Time.deltaTime;

        characterController.Move(movement);

        float yaw = Input.GetAxis("Mouse X") * sensitivity;
        float pitch = -Input.GetAxis("Mouse Y") * sensitivity;
        
        transform.Rotate(0, yaw, 0);
        CameraObj.transform.Rotate(pitch, 0, 0);
        //Add limit to turn later
    }
    private int GetPlatform()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
                return 1;
            case RuntimePlatform.Android:
                return 2;
/*            case RuntimePlatform.WebGLPlayer: currently broken will fix later stage
                return 5;*/
            default:
                return 0;
        }
    }
}
