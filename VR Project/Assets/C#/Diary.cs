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
    new void Using()
    {
        Debug.Log("日記閱讀中");
    }
}
