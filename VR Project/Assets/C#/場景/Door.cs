using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : ObjectInteractive
{
    public Animator dooranim;
    public bool openorclose = false;
    public Rigidbody door_rigidbody;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        
    }
    //當 互動鍵被按下時
    public override void Keydown()
    {
        door_rigidbody.constraints = RigidbodyConstraints.None;
        if (!openorclose)
        {
            dooranim.SetBool("開門", true);
            openorclose = !openorclose;
        }
        else
        {
            dooranim.SetBool("開門", false);
            openorclose = !openorclose;
        }
        Debug.Log("嘗試開關門");
        StopAllCoroutines();
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(8f);
        door_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
