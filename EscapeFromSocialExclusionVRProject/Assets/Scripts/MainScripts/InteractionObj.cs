using UnityEngine;

public abstract class InteractionObj : MonoBehaviour
{
    public float ringSizeOverride;
    public float gazeDurationOverride = 2f;
    public AudioClip interactionSound;

    private AudioSource audioSource; // reference to the AudioSource component


    private void Start()
    {
        // Add an AudioSource component if it doesn't already exist
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the interactionSound clip to the AudioSource
        audioSource.clip = interactionSound;
    }

    public void OnPointerEnter()
    {
    }

    public void OnPointerExit()
    {
    }

    public void OnPointerClick()
    {
        ClickFunction();
        PlayInteractionSound();
    }
    // Play the interaction sound through the AudioSource component
    public void PlayInteractionSound()
    {
        if (audioSource != null && interactionSound != null)
        {
            audioSource.PlayOneShot(interactionSound);
        }
    }
    public virtual void ClickFunction()
    {

    }
}
