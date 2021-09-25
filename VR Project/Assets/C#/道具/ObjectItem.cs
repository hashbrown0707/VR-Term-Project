using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectItem : ObjectInteractive
{
    
    protected string[] state = new string[3] { "未發現", "已發現", "使用中" };
    public string floor_tag = "地板";
    public string now_state;
    public int state_i = 0;
    public GameObject keep_slot;
    public GameObject table_slot;
    protected GameObject parent;
    public float fly_speed = 0.01f;
    public GameObject name;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.tag = "可拿取";
    }

    // Update is called once per frame
    protected void Update()
    {
        now_state = state[state_i];
    }

    //玩家看到物品後顯示名稱數秒(拿取光束)
    public void OnPlayerLookAt()
    {
        name.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(wait());
    }
    IEnumerator wait(int n=3)
    {
        yield return new WaitForSeconds(n);
        name.SetActive(false);
    }

    //物品落地後 回歸原位
    protected void OnCollisionEnter(Collision collision)
    {
        if(state_i == 1 && collision.gameObject.tag == floor_tag)
        {
            putit();
        }
    }

    //使用中
    public abstract void Using();
    //{
        //Debug.Log("ObjectItem 使用中");
    //}

    //解除使用
    public abstract void Unusing();
    //{
        //Debug.Log("ObjectItem 解除使用");
    //}

    //當 被拿取時
    public void keepit(GameObject it)
    {
        Debug.Log("ObjectItem 被拿取");
        
        if (keep_slot.TryGetComponent(out RotateKeep rotateKeep))
        {
            rotateKeep.ResetRotateAndPos();
        }

        ObjectMove itm = it.AddComponent<ObjectMove>();
        itm.PathA = it;
        itm.PathB = keep_slot;
        itm.Obj = it;

        /*int temp = 100;
        while (temp-->0)
            it.transform.position = Vector3.Lerp(it.transform.position, keep_slot.transform.position, fly_speed);*/

        //wait(1);


        parent = it.transform.parent.gameObject;
        it.transform.SetParent(keep_slot.transform);
        it.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

    }

    private IEnumerator wait(float sec)
    {
        yield return new WaitForSeconds(sec);
    }

    //當 被放下時
    public virtual void putit()
    {
        Unusing();
        Debug.Log("ObjectItem 被放下");
        
        GameObject it = this.gameObject;
        ObjectMove itm;
        if (it.TryGetComponent(out ObjectMove objectMove))
            itm = objectMove;
        else
            itm = it.AddComponent<ObjectMove>();

        if (keep_slot.TryGetComponent(out RotateKeep rotateKeep))
        {
            rotateKeep.ResetRotateAndPos();
        }

        itm.PathA = it;
        itm.PathB = table_slot;
        itm.Obj = it;
        Destroy(itm, 0.5f);

        it.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        it.transform.SetParent(parent.transform);

    }
}
