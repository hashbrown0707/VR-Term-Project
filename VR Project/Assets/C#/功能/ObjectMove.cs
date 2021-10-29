using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 物件移動的組件
/// </summary>
public class ObjectMove : MonoBehaviour
{

    private GameObject PathA;//起點
    private GameObject PathB;//終點
    private GameObject Obj;//要移動的物件
    private float speed;//移動速度
    private float firstSpeed;//紀錄第一次移動的距離
    public bool destory = false;

    /// <summary>
    /// 設定 物件o 由 a點 移動到 b點
    /// </summary>
    /// <param name="o">被移動的物件</param>
    /// <param name="a">起點</param>
    /// <param name="b">終點</param>
    /// <param name="s">移動速度</param>
    public void set(GameObject o, GameObject a, GameObject b, float s = 200f) 
    {
        Obj = o;
        PathA = a;
        PathB = b;
        speed = s;
    }

    private void Start()
    {
        // PathA 和 PathB 的距離乘上 speed
        firstSpeed = Vector3.Distance(Obj.transform.position, PathB.transform.position) * speed * Time.deltaTime;
    }

    private void Update()
    {
        //讓使用者每按一次 ↑ 時都移動一次，這只是為了方便看出每次移動的距離
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //先移動過後，再計算新的 speed
            Obj.transform.position = Vector3.Lerp(Obj.transform.position, PathB.transform.position, speed * Time.deltaTime);
            speed = calculateNewSpeed();
            if (speed == 0 && destory)
                Destroy(this);
        }
    }

    private float calculateNewSpeed()
    {
        //因為每次移動都是 Obj 在移動，所以要取得 Obj 和 PathB 的距離
        float tmp = Vector3.Distance(Obj.transform.position, PathB.transform.position);

        //當距離為0的時候，就代表已經移動到目的地了。
        if (tmp == 0)
            return tmp;
        else
            return (firstSpeed / tmp);
    }
}
