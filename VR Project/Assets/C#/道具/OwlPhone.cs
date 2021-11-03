using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwlPhone : ObjectItem
{
    public bool power = false;
    public GameObject charger;
    public GameObject power_slot;
    public bool unlock = false;
    public GameObject password;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = this.gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();
        text.text = "電量：0%";
    }
    private void Update()
    {
        unlock = !password.GetComponent<PasswordSYS>().pwlock;
    }

    //當 互動鍵被按下時
    public override void Keydown()
    {
        int temp_s = GetState();
        if (temp_s == 0)
            SetState(1);

        /*if (unlock)
        {

        }
        else if (power)
        {

        }
        else
        {

        }*/

        
        if (temp_s == 2)
        {
            SetState(3);
        }
        else if (temp_s == 3)
        {
            SetState(2);
        }
    }

    //使用中
    public override void Using()
    {
        //SetState(3);
        Debug.Log("手機使用中");

        if (unlock)
        {
            text.text = "我也不知道\n門的密碼030";
        }
        else if (power)
        {
            password.GetComponent<PasswordSYS>().openpasswordtable();
        }
        else
        {
            if (charger.GetComponent<WirelessCharger>().GetState() != 0)
            {
                ObjectMove itm;
                if (TryGetComponent(out ObjectMove objectMove))
                    itm = objectMove;
                else
                    itm = gameObject.AddComponent<ObjectMove>();

                if (keep_slot.TryGetComponent(out RotateKeep rotateKeep))
                    rotateKeep.ResetRotateAndPos();

                if (power_slot != null)
                    itm.set(gameObject, gameObject, power_slot);

                itm.destory = true;

                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                transform.SetParent(parent.transform);

                if (FindObjectOfType<mousetake>())
                    mousetake.op_port.ReleaseKeep();

                StartCoroutine(charge());
            }
        }
        
    }

    public override void Unusing()
    {
        //SetState(1);
        Debug.Log("手機解除使用");
    }
    public override void SetState(int s)
    {
        if (!(GetState() == 4 && s == 2))
            base.SetState(s);
    }
    IEnumerator charge()
    {
        SetState(4);
        charger.GetComponent<WirelessCharger>().SetState(4);
        for (int i = 0; i < 100; i++)
        {
            text.text = "電量：" + i + "%";
            yield return new WaitForSeconds(0.1f);
        }
        power = true;
        text.text = "鎖定畫面";
        //Unusing();
        charger.GetComponent<WirelessCharger>().SetState(1);
        PutIt();
        SetState(1);
    }
}
