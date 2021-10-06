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

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        /*if (state_i == 1 && !owlPhone.power)
        {
            owlPhone.power = true;
            owlPhone.text.text = "鎖定畫面";
        }
        else */if (state_i == 0 && !owlPhone.power)
        {
            owlPhone.text.text = "電量：0%";
        }
            
    }
    public override void Keydown()
    {
        if (state_i == 0)
            state_i = 1;

        if (owlpple.transform.parent == keep_slot.transform && state_i == 1 && !owlPhone.power) 
        {
            Using();
            StartCoroutine("charge");
        }
    }

    //使用中
    public override void Using()
    {
        Debug.Log("充電器使用中");
    }

    //解除使用
    public override void Unusing()
    {
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
        Unusing();
    }
}
