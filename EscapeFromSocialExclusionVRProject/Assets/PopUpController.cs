using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpController : MonoBehaviour
{
    public Image Image;
    public Sprite Sprite;
    public PopUpButton Button;
    // Start is called before the first frame update
    void Start()
    {
        Image.sprite = Sprite;
    }
    public void changeSprite(Sprite sprite)
    {
        Image.sprite = sprite;
    }
    public virtual void Clicked()
    {
        if (Button.clickstatus == true)
        {
            Destroy(this.gameObject);
        }
    }
}
