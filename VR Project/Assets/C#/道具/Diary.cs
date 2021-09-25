using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : ObjectItem
{
    //當 互動鍵被按下時
    public override void Keydown()
    {
        if (state_i == 0)
            state_i = 1;
        if (state_i == 1)
        {
            state_i = 2;
            Using();
            state_i = 1;
        }
    }

    //使用中
    public override void Using()
    {
        Debug.Log("日記閱讀中");
    }

    //解除使用
    public override void Unusing()
    {
        Debug.Log("放下日記");
    }
}
