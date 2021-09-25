using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunglasses : ObjectItem
{
    public GameObject diary;
    public Outline diary_HL;

    //當 互動鍵被按下時
    public override void Keydown()
    {
        if (state_i == 0)
            state_i = 1;
        if (state_i == 1)
        {
            state_i = 2;
            Using();
        }
        else if (state_i == 2)
        {
            Unusing();
            state_i = 1;
        }
    }

    //使用中
    public override void Using()
    {
        Debug.Log("墨鏡使用中");
        diary_HL.enabled = true;
    }

    //解除使用
    public override void Unusing()
    {
        Debug.Log("墨鏡解除使用");
        diary_HL.enabled = false;
    }
}
