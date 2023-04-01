using UnityEngine;

public abstract class InteractionObj : MonoBehaviour
{
    private Renderer _myRenderer;
    private Material _originalMaterial;
    private Material _outlinedMaterial;

    void Start()
    {
        _myRenderer = GetComponent<Renderer>();
        if (_myRenderer != null)
        {
            _originalMaterial = _myRenderer.material;
            _outlinedMaterial = new Material(_originalMaterial);
            _outlinedMaterial.shader = Shader.Find("Custom/OutlineShader");
            SetMaterial(false);
        }
    }

    public void OnPointerEnter()
    {
        SetMaterial(true);
    }

    public void OnPointerExit()
    {
        SetMaterial(false);
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
        if (_myRenderer == null) return;

        Material inactiveMaterial = _originalMaterial;
        Material activeMaterial = _outlinedMaterial;

        if (!gazedAt)
        {
            // Set the material to the original material if not gazed at
            _myRenderer.material = inactiveMaterial;
            return;
        }

        // Create a new material with only the outline
        Material outlineMaterial = new Material(_outlinedMaterial);
        outlineMaterial.SetFloat("_Outline", _outlinedMaterial.GetFloat("_Outline"));
        outlineMaterial.SetColor("_Color", _outlinedMaterial.GetColor("_OutlineColor"));
        outlineMaterial.SetColor("_OutlineColor", _outlinedMaterial.GetColor("_OutlineColor"));
        outlineMaterial.mainTexture = _outlinedMaterial.mainTexture;
        outlineMaterial.SetTexture("_ToonShade", _outlinedMaterial.GetTexture("_ToonShade"));

        // Set the material to the outline material
        _myRenderer.material = outlineMaterial;
    }

}
