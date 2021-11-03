using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public abstract class ObjectItem : ObjectInteractive
{
    /// <summary>
    /// 0="未發現" 1="已發現" 2="拿取中" 3="使用中"
    /// </summary>
    int state = 0;
    public string floor_tag = "地板";
    public GameObject keep_slot;
    public GameObject table_slot;
    protected GameObject parent;
    public float fly_speed = 0.01f;
    private GameObject show_name;
    public GameObject name_prefab;
    public GameObject content_prefab;

    [Tooltip("敘述：\n0被發現時\n1拿取時\n2使用時\n3用完時\n4放下時\nPS:目前只實裝0 \\n換行")]
    public string[] lines;

    void Awake()
    {
        this.gameObject.tag = "可拿取";
        /*var cp = this.gameObject.AddComponent<ContentPrinter>();
        if (gameObject.transform.childCount >= 2)
            cp.canvas = gameObject.transform.GetChild(1).gameObject;*/

        var np = Instantiate(name_prefab, this.transform, false);
        np.transform.Find("Text").GetComponent<Text>().text = this.name;
        show_name = np;

        var cp = Instantiate(content_prefab, this.transform, false);
        
        var contentprinter = this.gameObject.AddComponent<ContentPrinter>();
        contentprinter.canvas = cp;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    Coroutine ptr_show_name_coroutine = null;
    /// <summary>
    /// 顯示道具名稱 n 秒
    /// </summary>
    /// <param name="n">秒</param>
    void ShowNameForSec(float n)
    {
        if (transform.parent != keep_slot.transform)
        {
            show_name.SetActive(true);
            if (ptr_show_name_coroutine != null)
                StopCoroutine(ptr_show_name_coroutine);
            ptr_show_name_coroutine = StartCoroutine(ShowNameCoroutine(n));
        }
        else
            show_name.SetActive(false);
    }

    /// <summary>
    /// 道具名稱持續顯示
    /// </summary>
    /// <param name="n">秒</param>
    /// <returns></returns>
    IEnumerator ShowNameCoroutine(float n)
    {
        yield return new WaitForSeconds(n);
        show_name.SetActive(false);
        ptr_show_name_coroutine = null;
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
    public virtual void KeepIt()
    {
        Debug.Log("ObjectItem 被拿取");
        if (keep_slot.TryGetComponent(out RotateKeep rotateKeep))
            rotateKeep.ResetRotateAndPos();
        if (keep_slot.TryGetComponent(out RotateKeepVR rotateKeepVR))
            rotateKeepVR.ResetRotateAndPos();

        ObjectMove itm = gameObject.AddComponent<ObjectMove>();

        itm.set(gameObject, gameObject, keep_slot);
        parent = transform.parent.gameObject;
        transform.SetParent(keep_slot.transform);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        keep_slot.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    /// <summary>
    /// 當道具被放下時
    /// </summary>
    public virtual void PutIt()
    {
        Debug.Log("ObjectItem 被放下");
        ObjectMove itm;
        if (TryGetComponent(out ObjectMove objectMove))
            itm = objectMove;
        else
            itm = gameObject.AddComponent<ObjectMove>();

        if (keep_slot.TryGetComponent(out RotateKeep rotateKeep))
            rotateKeep.ResetRotateAndPos();
        if (keep_slot.TryGetComponent(out RotateKeepVR rotateKeepVR))
            rotateKeepVR.ResetRotateAndPos();

        if (table_slot != null)
            itm.set(gameObject, gameObject, table_slot);
        
        itm.destory = true;

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        transform.SetParent(parent.transform);
    }

    /// <summary>
    /// 0="未發現" 1="已發現" 2="拿取中" 3="使用中"
    /// </summary>
    public int GetState()
    {
        return state;
    }

    /// <summary>
    /// 設定狀態 0="未發現" 1="已發現" 2="拿取中" 3="使用中"
    /// </summary>
    public virtual void SetState(int s)
    {
        if(state != s)
        {
            if (state == 0 && s == 2)
            {
                OnBeFound();
                OnTake();
            }
            else if (state == 0)
                OnBeFound();
            else if (state == 1 && s == 2)
                OnTake();
            else if (state == 1 && s == 3)
                OnUse();
            else if (state == 2 && s == 1)
                OnPut();
            else if (state == 2 && s == 3)
                OnUse();
            else if (state == 3 && s == 1)
            {
                OnUnuse();
                OnPut();
            }
            else if (state == 3 && s == 2)
                OnUnuse();

            state = s;
        }
    }

    /// <summary>
    /// 當玩家看到道具
    /// </summary>
    public void OnPlayerLookAt()
    {
        ShowNameForSec(3);
    }

    /// <summary>
    /// 物品落地後 回歸原位
    /// </summary>
    /// <param name="collision"></param>
    protected void OnCollisionEnter(Collision collision)
    {
        if (state == 1 && collision.gameObject.CompareTag(floor_tag))
            PutIt();
    }

    /// <summary>
    /// 被發現時
    /// </summary>
    protected virtual void OnBeFound()
    {
        if (lines.Length > 0 && lines[0] != null && TryGetComponent<ContentPrinter>(out var cp))
            cp.SetAndPlay(lines[0].Replace("\\n","\n"));
            
    }

    /// <summary>
    /// 被拿取時
    /// </summary>
    protected virtual void OnTake()
    {
        KeepIt();
    }

    /// <summary>
    /// 被放下時
    /// </summary>
    protected virtual void OnPut()
    {
        PutIt();
    }

    /// <summary>
    /// 被使用時
    /// </summary>
    protected virtual void OnUse()
    {
        Using();
    }

    /// <summary>
    /// 使用完時
    /// </summary>
    protected virtual void OnUnuse()
    {
        Unusing();
    }
}
