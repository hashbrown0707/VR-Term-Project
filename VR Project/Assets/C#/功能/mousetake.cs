using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousetake : MonoBehaviour
{
    public GameObject player;
    public float t = 0.1f;
    public float 倍率 = 1.0005f;
    public int count = 1;
    public int max_count = 600;
    public string interactivetag;

    private Camera cam;//發射射線的攝像機
    private GameObject go;//射線碰撞的物體
    private Vector3 screenSpace;
    private Vector3 offset;

    private bool isdrage = false;
    private bool keepobject = false;
    private GameObject keep;

    public int take_cd = 0;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (isdrage == false)
        {
            //整體初始位置 
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            //從攝像機發出到點選座標的射線
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                //劃出射線，只有在scene檢視中才能看到
                Debug.DrawLine(ray.origin, hitInfo.point, Color.red);

                go = hitInfo.collider.gameObject;
                screenSpace = cam.WorldToScreenPoint(go.transform.position);
                offset = go.transform.position - cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));

            }
        }

        //拖拽物體不能為空
        if (go != null)
        {
            // 按下互動鍵"f"
            if (Input.GetKeyDown("f"))
            {
                if (go.gameObject.TryGetComponent(out ObjectInteractive objectInteractive))
                {
                    objectInteractive.Keydown();
                }
            }
            
            if(go.gameObject.TryGetComponent(out ObjectItem oi))
            {
                oi.OnPlayerLookAt();
            }

            //短按 拿到面前
            if (Input.GetMouseButtonDown(0) && !keepobject && take_cd == 0 && go.tag == interactivetag)
            {
                if (go.gameObject.TryGetComponent(out ObjectItem objectItem))
                {
                    objectItem.state_i = 1;
                    objectItem.keepit(go);
                    keep = go;
                    keepobject = !keepobject;
                    take_cd = 60;
                }

            }
            //短按 放下
            else if (Input.GetMouseButtonDown(0) && keepobject)
            {
                if (keep.gameObject.TryGetComponent(out ObjectItem objectItem))
                {
                    objectItem.putit();
                    keepobject = !keepobject;
                    keep = null;
                    take_cd = 0;
                }
            }
            //長按 拖拽
            else if (Input.GetMouseButton(0) && take_cd == 0 && go.tag == interactivetag)
            {
                if (keep != null && keep.gameObject.TryGetComponent(out ObjectItem keepobjectItem))
                {
                    keepobjectItem.putit();
                    keepobject = !keepobject;
                    keep = null;
                    take_cd = 0;
                }

                Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
                Vector3 currentPosition = cam.ScreenToWorldPoint(currentScreenSpace) + offset;

                go.transform.position = currentPosition;
                isdrage = true;

                //拖曳後轉變狀態
                if (go.gameObject.TryGetComponent(out ObjectItem objectItem))
                    objectItem.state_i = 1;
                    
            }
            //魯味的鉤
            else if (Input.GetMouseButton(4))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                Physics.Raycast(ray, out hitInfo);

                Vector3 currentPosition = new Vector3(hitInfo.point.x, player.transform.position.y, hitInfo.point.z);

                float temp_t = t;
                for (int i = 0; i < count; i++)
                    temp_t *= 倍率;
                Debug.Log(temp_t + "/" + count);
                player.transform.position = Vector3.Lerp(player.transform.position, currentPosition, temp_t * Time.deltaTime);
                count = (count < max_count) ? count + 1 : max_count;
                isdrage = true;
            }
            else
            {
                //結束後，清空物體
                go = null;
                isdrage = false;
                count = 1;
            }
            take_cd = (take_cd == 0) ? 0 : take_cd - 1;
        }
        
    }
}
