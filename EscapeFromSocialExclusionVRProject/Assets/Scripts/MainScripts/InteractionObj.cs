using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionObj : MonoBehaviour
{
    public Renderer _myRenderer;
    /// <summary>
    /// The material to use when this object is inactive (not being gazed at).
    /// </summary>
    public Material InactiveMaterial;

    /// <summary>
    /// The material to use when this object is active (gazed at).
    /// </summary>
    public Material GazedAtMaterial;
    // Start is called before the first frame update
    void Start()
    {
        if (!_myRenderer)
            _myRenderer = GetComponent<Renderer>();
        SetMaterial(false);
    }
    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnter()
    {
        SetMaterial(true);
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExit()
    {
        SetMaterial(false);
    }

    /// <summary>
    /// This method is called by the Main Camera when it is gazing at this GameObject and the screen
    /// is touched.
    /// </summary>
    public void OnPointerClick()
    {
        ClickFunction();
    }

    public virtual void ClickFunction()
    {

    }
    /// <summary>
    /// Sets this instance's material according to gazedAt status.
    /// </summary>
    ///
    /// <param name="gazedAt">
    /// Value `true` if this object is being gazed at, `false` otherwise.
    /// </param>
    private void SetMaterial(bool gazedAt)
    {
        if (InactiveMaterial != null && GazedAtMaterial != null)
        {
            if (gazedAt)
                _myRenderer.material = GazedAtMaterial;
            else
                _myRenderer.material = InactiveMaterial;
        }
    }
}
