using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class CameraPointer : MonoBehaviour
{
    private const float _maxDistance = 50f;
    public GameObject _gazedAtObject = null;
    private float _gazeTime = 0f;
    private bool _gazing = false;
    private float _fillAmount = 0f;
    public GameObject _ringPrefab;
    private GameObject _ring;
    public float _gazeDuration = 2f;
    public bool AutoClickEnabled = true;

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            // GameObject detected in front of the camera.
            if (hit.transform.gameObject != _gazedAtObject && hit.transform.gameObject.tag == "Interactable")
            {
                // New GameObject.
                if (_gazedAtObject != null)
                {
                    _gazedAtObject.SendMessage("OnPointerExit");
                }
                _gazedAtObject = hit.transform.gameObject;
                _gazedAtObject.SendMessage("OnPointerEnter");

                // Create ring.
                CreateRing(_gazedAtObject.transform.localScale.magnitude);

                // Gaze.
                _gazeTime = Time.time;
                _gazing = true;
            }
            else if (hit.transform.gameObject.tag != "Interactable")
            {
                if (_gazedAtObject)
                {
                    _gazedAtObject.SendMessage("OnPointerExit");
                    _gazedAtObject = null;
                }
                DestroyRing();
                _gazing = false;
                _fillAmount = 0f;
                _gazeTime = 0f;
            }
        }
        else
        {
            // No GameObject detected in front of the camera.
            if (_gazedAtObject && _gazedAtObject.GetComponent<InteractionObj>())
            {
                _gazedAtObject.SendMessage("OnPointerExit");
                _gazedAtObject = null;
            }
            DestroyRing();
            _gazing = false;
            _fillAmount = 0f;
            _gazeTime = 0f;
        }

        if (_gazedAtObject)
        {
            if (_gazing && AutoClickEnabled)
            {
                _fillAmount = (Time.time - _gazeTime) / _gazeDuration;
                if (Time.time - _gazeTime > _gazeDuration)
                {
                    _gazedAtObject.SendMessage("OnPointerClick");
                    _gazing = false;
                    _fillAmount = 0f;
                }
                UpdateRing(_fillAmount);
            }
            if (_gazing && Input.GetMouseButtonDown(0))
            {
                _gazedAtObject.SendMessage("OnPointerClick");
            }

            // Checks for screen touches.
            if (Google.XR.Cardboard.Api.IsTriggerPressed)
            {
                _gazedAtObject?.SendMessage("OnPointerClick");
            }
        }
    }

    /// <summary>
    /// Creates a ring based on the size of the object being gazed at.
    /// </summary>
    /// <param name="size">The size of the object being gazed at.</param>
    /// 
    private RingController ringController;
    private void CreateRing(float size)
    {
        if (size < 0.7f)
            size = 0.7f;
        if (_gazedAtObject.GetComponent<InteractionObj>().ringSizeOverride > 0f)
            size = _gazedAtObject.GetComponent<InteractionObj>().ringSizeOverride;
        DestroyRing();
        _ring = Instantiate(_ringPrefab, _gazedAtObject.transform.position, Quaternion.identity);
        _ring.transform.localScale = new Vector3(size, size, size);
        _ring.transform.LookAt(this.transform);
        // Set up the ring's image fill amount
        Image ringImage = _ring.GetComponent<Image>();
        if (ringImage != null)
        {
            ringImage.type = Image.Type.Filled;
            ringImage.fillMethod = Image.FillMethod.Radial360;
            ringImage.fillOrigin = (int)Image.Origin360.Top;
            ringImage.fillAmount = 0f;
        }
        ringController = _ring.GetComponent<RingController>();
        Canvas canvas = _ring.GetComponent<Canvas>();
        canvas.sortingOrder = 999;
    }

    private void UpdateRing(float fillAmount)
    {
        _ring.transform.LookAt(this.transform);
        ringController.Fill.fillAmount = fillAmount;
    }

    private void DestroyRing()
    {
        if (_ring != null)
        {
            Destroy(_ring);
            _ring = null;
        }
    }
}