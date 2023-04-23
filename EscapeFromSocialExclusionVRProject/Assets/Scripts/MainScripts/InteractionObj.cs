using UnityEngine;

public abstract class InteractionObj : MonoBehaviour
{
    private Outline outline;
    public Outline.Mode OutlineMode = Outline.Mode.OutlineAndSilhouette;
    public float ringSizeOverride;
    public float gazeDurationOverride = 2f;

    void Start()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = OutlineMode;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 10f;
    }

    public void OnPointerEnter()
    {
        outline.ManualUpdate();
        outline.enabled = true;
    }

    public void OnPointerExit()
    {
        outline.enabled = false;
    }

    public void OnPointerClick()
    {
        ClickFunction();
    }

    public virtual void ClickFunction()
    {

    }

    private void SetMaterial(bool gazedAt)
    {
       
    }

}
