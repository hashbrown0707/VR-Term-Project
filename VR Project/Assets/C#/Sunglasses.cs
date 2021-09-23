using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunglasses : ObjectItem
{
    public GameObject diary;
    public GameObject diary_HL;
    private Diary dd;
    private Light dhl;

    private void Start()
    {
        dd = diary.GetComponent<Diary>();
        dhl = diary_HL.GetComponent<Light>();
    }

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
    new void Using()
    {
        Debug.Log("墨鏡使用中");
        dhl.enabled = true;
    }

    //解除使用
    new void Unusing()
    {
        Debug.Log("墨鏡解除使用");
        dhl.enabled = false;
    }
}
