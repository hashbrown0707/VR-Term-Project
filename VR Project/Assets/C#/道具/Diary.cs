using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : ObjectItem
{
    //當 互動鍵被按下時
    public override void Keydown()
    {
        int temp_s = GetState();
        if (temp_s == 0)
            SetState(1);
        if (temp_s == 2)
        {
            Using();
        }
        else if(temp_s == 3)
        {
            Unusing();
        }
    }

    //使用中
    public override void Using()
    {
        SetState(3);
        Debug.Log("日記閱讀中");
    }

    //解除使用
    public override void Unusing()
    {
        SetState(1);
        Debug.Log("放下日記");
    }

    protected override void OnBeFound()
    {
        base.OnBeFound();
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
}
