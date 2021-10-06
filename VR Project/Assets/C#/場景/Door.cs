using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : ObjectInteractive
{
    public Animator dooranim;
    public bool openorclose = false;
    public Rigidbody door_rigidbody;
    
    //當 互動鍵被按下時
    public override void Keydown()
    {
        door_rigidbody.constraints = RigidbodyConstraints.None;
        
        dooranim.SetBool("開門", !openorclose);
        openorclose = !openorclose;

        Debug.Log("嘗試開關門");
        if (ptr_freeze_cd != null)
            StopCoroutine(ptr_freeze_cd);
        ptr_freeze_cd = StartCoroutine(freeze_cd());
    }

    Coroutine ptr_freeze_cd = null;

    /// <summary>
    /// 經過8秒後 鎖定門的剛體
    /// </summary>
    /// <returns></returns>
    IEnumerator freeze_cd()
    {
        yield return new WaitForSeconds(8f);
        door_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        ptr_freeze_cd = null;
    }
}
