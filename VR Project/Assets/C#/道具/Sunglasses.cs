using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunglasses : ObjectItem
{
    public GameObject diary;
    public Outline diary_HL;
    private bool used = false;

    //當 互動鍵被按下時
    public override void Keydown()
    {
        int temp_s = GetState();
        if (temp_s == 0)
            SetState(1);
        if (temp_s == 2)
        {
            SetState(3);
        }
        else if (temp_s == 3)
        {
            SetState(2);
        }
    }

    //使用中
    public override void Using()
    {
        if (!used)
        {
            ImgManager.op_port.SwitchImgManager(0, 1);
            used = true;
        }
        
        Debug.Log("墨鏡使用中");
        diary_HL.enabled = true;
        if (FindObjectOfType<ChainMoveVR>())
            ChainMoveVR.keep = null;
        Destroy(gameObject);
    }

    //解除使用
    public override void Unusing()
    {
        Debug.Log("墨鏡解除使用");
        diary_HL.enabled = false;
    }

    protected override void OnBeFound()
    {
        base.OnBeFound();
        
        /*if(this.TryGetComponent<ContentPrinter>(out var cp))
        {
            cp.Set("這是一個\n能看到日記的\n墨鏡");
            cp.Play();
        }*/
    }
}
