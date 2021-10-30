using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwlPhone : ObjectItem
{
    public bool power = false;
    public GameObject password;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = this.gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();
        text.text = "電量：0%";
    }
    //當 互動鍵被按下時
    public override void Keydown()
    {
        int temp_s = GetState();
        if (temp_s == 0)
            SetState(1);
        if (power && temp_s == 2)
        {
            Using();
        }
        else if (power && temp_s == 3)
        {
            Unusing();
        }
    }

    //使用中
    public override void Using()
    {
        SetState(3);
        Debug.Log("手機使用中");
        password.GetComponent<PasswordSYS>().openpasswordtable();
    }

    public override void Unusing()
    {
        SetState(1);
        Debug.Log("手機解除使用");
    }
    
}
