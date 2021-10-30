using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEvent
{
    /// <summary>
    /// 0="未發現" 1="已發現" 2="拿取中" 3="使用中"
    /// </summary>
    int state = 0;

    /// <summary>
    /// 0="未發現" 1="已發現" 2="拿取中" 3="使用中"
    /// </summary>
    public int GetState()
    {
        return state;
    }

    /// <summary>
    /// 進入下一個狀態
    /// </summary>
    public void SetState(int s)
    {
        if (state == 0)
            OnBeFound();
        else if (state == 1)
            OnTake();
        state = s;
    }

    /// <summary>
    /// 被發現時
    /// </summary>
    void OnBeFound()
    {

    }

    /// <summary>
    /// 被拿取時
    /// </summary>
    void OnTake()
    {

    }

}
