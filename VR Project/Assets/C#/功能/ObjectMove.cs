using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{

    public GameObject PathA;//起點
    public GameObject PathB;//終點
    public GameObject Obj;//要移動的物件
    public float speed = 200f;//移動速度
    private float firstSpeed;//紀錄第一次移動的距離

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
