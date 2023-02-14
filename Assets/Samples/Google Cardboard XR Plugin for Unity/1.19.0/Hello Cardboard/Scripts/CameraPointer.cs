//-----------------------------------------------------------------------
// <copyright file="CameraPointer.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class CameraPointer : MonoBehaviour
{
    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;
    private float _gazeTime = 0f;
    public bool _gazing = false;
    private float _fillAmount = 0f;
    public Image _PointerImage;
    public Animator _PointerAnimator;
    public float _gazeDuration = 2f;

    private void Start()
    {
    }


    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            
            // GameObject detected in front of the camera.
            if (hit.transform.gameObject != _gazedAtObject && hit.transform.gameObject.tag == "Interactable")
            {
                // New GameObject.
                _gazedAtObject?.SendMessage("OnPointerExit");
                _gazedAtObject = hit.transform.gameObject;
                _gazedAtObject.SendMessage("OnPointerEnter");

                //Gaze
                _gazeTime = Time.time;
                _gazing = true;
            }
            else if (hit.transform.gameObject.tag != "Interactable")
            {
                if (_gazedAtObject)
                {
                    _gazedAtObject?.SendMessage("OnPointerExit");
                    _gazedAtObject = null;
                }
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
                _gazedAtObject?.SendMessage("OnPointerExit");
                _gazedAtObject = null;
            }
            _gazing = false;
            _fillAmount = 0f;
            _gazeTime = 0f;
        }

        if (_gazing)
        {
            _fillAmount = (Time.time - _gazeTime) / _gazeDuration;
            if (Time.time - _gazeTime > _gazeDuration)
            {
                _gazedAtObject.SendMessage("OnPointerClick");
                _gazing = false;
                _fillAmount = 0f;
            }
        }

        _PointerImage.fillAmount = _fillAmount;
        _PointerAnimator.SetBool("Gaze", _gazing);

        // Checks for screen touches.
        if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            _gazedAtObject?.SendMessage("OnPointerClick");
        }
    }
    private IEnumerator CheckGaze()
    {
        while (_gazedAtObject != null)
        {
            if (Time.time - _gazeTime >= 2)
            {
                _gazedAtObject.SendMessage("OnPointerClick");
                yield break;
            }

            yield return null;
        }
    }
}
