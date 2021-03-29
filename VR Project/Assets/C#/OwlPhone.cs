using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwlPhone : ObjectEvent
{
    public bool power = false;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = this.gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();
    }
    new void Update()
    {
        base.Update();
        if (power && state_i == 1)
            text.text = "鎖定畫面";
        else if (power && state_i == 2)
            text.text = "密碼：1234";
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
        else if(state_i == 2)
        {
            Unusing();
            state_i = 1;
        }
    }

    //使用中
    new void Using()
    {
        Debug.Log("手機使用中");
    }

    new void Unusing()
    {
        Debug.Log("手機解除使用");
    }
}
