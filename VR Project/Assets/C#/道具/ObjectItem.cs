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
    public GameObject show_name;

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
    
    /// <summary>
    /// 當玩家看到道具
    /// </summary>
    public void OnPlayerLookAt()
    {
        show_name_n_sec(3);
    }

    Coroutine ptr_show_name_coroutine = null;
    /// <summary>
    /// 顯示道具名稱 n 秒
    /// </summary>
    /// <param name="n">秒</param>
    void show_name_n_sec(float n)
    {
        if (this.gameObject.transform.parent != keep_slot.transform)
        {
            show_name.SetActive(true);
            if (ptr_show_name_coroutine != null)
                StopCoroutine(ptr_show_name_coroutine);
            ptr_show_name_coroutine = StartCoroutine(show_name_coroutine(n));
        }
        else
            show_name.SetActive(false);
    }

    /// <summary>
    /// 道具名稱持續顯示
    /// </summary>
    /// <param name="n">秒</param>
    /// <returns></returns>
    IEnumerator show_name_coroutine(float n)
    {
        yield return new WaitForSeconds(n);
        show_name.SetActive(false);
        ptr_show_name_coroutine = null;
    }

    /// <summary>
    /// 物品落地後 回歸原位
    /// </summary>
    /// <param name="collision"></param>
    protected void OnCollisionEnter(Collision collision)
    {
        if(state_i == 1 && collision.gameObject.tag == floor_tag)
            putit();
    }

    /// <summary>
    /// 使用道具中
    /// </summary>
    public abstract void Using();

    /// <summary>
    /// 解除使用道具
    /// </summary>
    public abstract void Unusing();

    /// <summary>
    /// 當道具被拿取時
    /// </summary>
    /// <param name="it"></param>
    public void keepit(GameObject it)
    {
        Debug.Log("ObjectItem 被拿取");
        if (keep_slot.TryGetComponent(out RotateKeep rotateKeep))
            rotateKeep.ResetRotateAndPos();

        ObjectMove itm = it.AddComponent<ObjectMove>();
        itm.set(it, it, keep_slot);
        parent = it.transform.parent.gameObject;
        it.transform.SetParent(keep_slot.transform);
        it.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        keep_slot.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    /// <summary>
    /// 當道具被放下時
    /// </summary>
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
            rotateKeep.ResetRotateAndPos();

        if (this.table_slot != null)
            itm.set(it, it, table_slot);
        
        itm.destory = true;

        it.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        it.transform.SetParent(parent.transform);
    }
}
