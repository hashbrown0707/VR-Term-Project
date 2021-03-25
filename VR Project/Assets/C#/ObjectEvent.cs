using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEvent : MonoBehaviour
{
    
    protected string[] state = new string[3] { "未發現", "已發現", "使用中" };
    public string now_state;
    public int state_i = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        now_state = state[state_i];
    }

    //當 互動鍵被按下時
    public virtual void Keydown()
    {
        Debug.Log("OE互動鍵被按下");
    }

    //使用中
    protected void Using()
    {
        Debug.Log("OE使用中");
    }

    //解除使用
    protected void Unusing()
    {
        Debug.Log("OE解除使用");
    }
}
