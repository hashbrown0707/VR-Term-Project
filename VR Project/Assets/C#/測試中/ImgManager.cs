using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImgManager : MonoBehaviour
{
    public static ImgManager op_port;
    private void Awake()
    {
        op_port = this;
    }
    public ImgCtrl[] imgCtrls;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchImgManager(int index_ctrl, int index_img)
    {
        if(index_ctrl < imgCtrls.Length)
        {
            imgCtrls[index_ctrl].SwitchImgCtrl(index_img);
        }
        else
        {
            Debug.LogError(this + "index out of range");
        }
    }
}
