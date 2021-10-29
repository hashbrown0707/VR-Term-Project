using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectInteractive : MonoBehaviour
{
    /// <summary>
    /// 當物件互動鍵被按下時
    /// </summary>
    public abstract void Keydown();
}
