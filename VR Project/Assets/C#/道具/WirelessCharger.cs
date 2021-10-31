using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirelessCharger: ObjectItem
{
    public GameObject owlpple;
    private OwlPhone owlPhone;
    void Start()
    {
        owlPhone = owlpple.GetComponent<OwlPhone>();
    }

    public override void Keydown()
    {
        int temp_s = GetState();
        if (temp_s == 0)
            SetState(1);

        if (owlPhone.GetState() == 2 && temp_s == 1 && !owlPhone.power) 
        {
            SetState(3);
            StartCoroutine("charge");
        }
    }

    //使用中
    public override void Using()
    {
        //SetState(2);
        Debug.Log("充電器使用中");
    }

    //解除使用
    public override void Unusing()
    {
        //SetState(1);
        Debug.Log("充電器解除使用");
    }

    IEnumerator charge()
    {
        for (int i = 0; i < 100; i++)
        {
            owlPhone.text.text = "電量：" + i + "%";
            yield return new WaitForSeconds(0.3f);
        }
        owlPhone.power = true;
        owlPhone.text.text = "鎖定畫面";
        //Unusing();
        SetState(1);
    }
}
