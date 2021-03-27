using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEvent : MonoBehaviour
{
    
    protected string[] state = new string[3] { "未發現", "已發現", "使用中" };
    public string floor_tag = "地板";
    public string now_state;
    public int state_i = 0;
    public GameObject keep_slot;
    public GameObject table_slot;
    protected GameObject parent;
    public float fly_speed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        now_state = state[state_i];
    }

    //物品落地後 回歸原位
    protected void OnCollisionEnter(Collision collision)
    {
        if(state_i == 1 && collision.gameObject.tag == floor_tag)
        {
            putit();
        }
    }

    //當 互動鍵被按下時
    public virtual void Keydown()
    {
        Debug.Log("OE互動鍵被按下");
    }

    //使用中
    protected void Using()
    {
        Debug.Log("OE使用中");
    }

    //解除使用
    protected void Unusing()
    {
        Debug.Log("OE解除使用");
    }

    //當 被拿取時
    public void keepit(GameObject it)
    {
        Debug.Log("OE被拿取");

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
        Debug.Log("OE被放下");
        
        GameObject it = this.gameObject;
        ObjectMove itm;
        if (it.TryGetComponent(out ObjectMove objectMove))
            itm = objectMove;
        else
            itm = it.AddComponent<ObjectMove>();
        itm.PathA = it;
        itm.PathB = table_slot;
        itm.Obj = it;
        Destroy(itm, 0.5f);

        it.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        it.transform.SetParent(parent.transform);

    }
}
