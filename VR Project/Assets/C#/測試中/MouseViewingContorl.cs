using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<Summary>注意：與CameraControl的功能衝突</Summary>
public class MouseViewingContorl : MonoBehaviour
{
    public GameObject camera_gameobject;
    public GameObject player;
    public float sensitivityX = 10f;
    public float sensitivityY = 10f;
    [SerializeField] KeyCode 視角控制btn = KeyCode.Mouse1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ViewingContorl();
    }

    /// <summary>
    /// 設定新的視角控制按鍵
    /// </summary>
    /// <param name="newbtn"></param>
    public void switch視角控制btn(KeyCode newbtn)
    {
        視角控制btn = newbtn;
    }

    void ViewingContorl()
    {
        if (Input.GetKey(視角控制btn))
        {
            float rotationX = Input.GetAxis("Mouse X") * sensitivityX;
            float rotationY = Input.GetAxis("Mouse Y") * sensitivityY;
            camera_gameobject.transform.Rotate(-rotationY, 0, 0);
            player.transform.Rotate(0, rotationX, 0);
        }
    }
}
