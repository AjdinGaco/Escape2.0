using UnityEngine;

public abstract class InteractionObj : MonoBehaviour
{
    public float ringSizeOverride;
    public float gazeDurationOverride = 2f;

    public void OnPointerEnter()
    {
    }

    public void OnPointerExit()
    {
    }

    public void OnPointerClick()
    {
        ClickFunction();
    }

    public virtual void ClickFunction()
    {

    }
}
