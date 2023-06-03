using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlindnessManager : MonoBehaviour
{
    public Image EffectsCanvas;
    public VisionDisabilities currentActive;
    public Sprite None,CataractsSprite, DiabeticRetinopathySprite, GlaucomaSprite, MacularDegenerationSprite;

    public void ChangeVisionDisabilities(VisionDisabilities visionDisability)
    {
        currentActive = visionDisability;
        switch (visionDisability)
        {
            case VisionDisabilities.None:
                EffectsCanvas.sprite = None;
                break;
            case VisionDisabilities.Cataracts:
                EffectsCanvas.sprite = CataractsSprite;
                break;
            case VisionDisabilities.DiabeticRetinopathy:
                EffectsCanvas.sprite = DiabeticRetinopathySprite;
                break;
            case VisionDisabilities.Glaucoma:
                EffectsCanvas.sprite = GlaucomaSprite;
                break;
            case VisionDisabilities.MacularDegeneration:
                EffectsCanvas.sprite = MacularDegenerationSprite;
                break;
        }
    }
}

public enum VisionDisabilities
{
    None,
    Cataracts,
    DiabeticRetinopathy,
    Glaucoma,
    MacularDegeneration
}
