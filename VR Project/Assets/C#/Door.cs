using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : ObjectEvent
{
    public Animator dooranim;

    // Start is called before the first frame update
    void Start()
    {

    }
    new void Update()
    {
        base.Update();
    }
    //當 互動鍵被按下時
    public override void Keydown()
    {
        if (state_i == 0)
        {
            dooranim.SetBool("開門", true);
            state_i = 1;
        }
        else
        {
            dooranim.SetBool("開門", false);
            state_i = 0;
        }
        Debug.Log("嘗試開關門");
    }

}
