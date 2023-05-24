using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablingClickObject : InteractionObj
{
    public GameObject GameObjectToEnable;
    public bool DestroyOnClick = false;

    public bool makeSoundUntillInteractedWith = false;
    private bool interacted = false;
    public InteractGate InteractGate;
    public override void ClickFunction()
    {
        if (GameObjectToEnable.active)
            GameObjectToEnable.SetActive(false);
        else
            GameObjectToEnable.SetActive(true);


        if (DestroyOnClick)
            Destroy(gameObject);
        
        if (!interacted)
        {
            interacted = true;
            _interactionAudioSource.Stop();
            if (InteractGate)
            {
                InteractGate.InteractionDone();
            }
        }
    }

    public AudioSource _interactionAudioSource;
    private void Awake()
    {
        if (makeSoundUntillInteractedWith)
        {
            _interactionAudioSource.Play();
        }

    }
}
