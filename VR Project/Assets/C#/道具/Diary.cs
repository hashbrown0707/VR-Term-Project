using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : ObjectItem
{
    public GameObject work_slot;
    //當 互動鍵被按下時
    public override void Keydown()
    {
        int temp_s = GetState();
        if (temp_s == 0)
            SetState(1);
        if (temp_s == 2)
        {
            SetState(3);
            SetState(4); // 進入工作區 應啟動日記翻頁的code(未實作)
        }
        else if(temp_s == 3)
        {
            SetState(2);
        }
    }

    //使用中
    public override void Using()
    {
        //SetState(3);
        Debug.Log("日記閱讀中");
        ObjectMove itm;
        if (TryGetComponent(out ObjectMove objectMove))
            itm = objectMove;
        else
            itm = gameObject.AddComponent<ObjectMove>();

        if (keep_slot.TryGetComponent(out RotateKeep rotateKeep))
            rotateKeep.ResetRotateAndPos();
        if (keep_slot.TryGetComponent(out RotateKeepVR rotateKeepVR))
            rotateKeepVR.ResetRotateAndPos();

        if (work_slot != null)
            itm.set(gameObject, gameObject, work_slot);

        itm.destory = true;

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        transform.SetParent(parent.transform);

        if (FindObjectOfType<mousetake>())
            mousetake.op_port.ReleaseKeep();
        if (FindObjectOfType<ChainMoveVR>())
            ChainMoveVR.keep = null;
    }

    //解除使用
    public override void Unusing()
    {
        //SetState(1);
        Debug.Log("放下日記");
    }

    protected override void OnBeFound()
    {
        base.OnBeFound();
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
    public override void SetState(int s)
    {
        if (!(GetState() >= 4))
            base.SetState(s);
        if (this.gameObject.GetComponent<Outline>().enabled)
            this.gameObject.GetComponent<Outline>().enabled = false;
    }
}
