using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImgCtrl : MonoBehaviour
{
    private SpriteRenderer m_sprite;
    public Sprite[] imgs;
    // Start is called before the first frame update
    void Start()
    {
        m_sprite = GetComponent<SpriteRenderer>();
    }

    public void SwitchImgCtrl(int index)
    {
        if(index < imgs.Length)
        {
            m_sprite.sprite = imgs[index];
        }
        else
        {
            Debug.LogError(this + "index out of range");
        }
    }

}
