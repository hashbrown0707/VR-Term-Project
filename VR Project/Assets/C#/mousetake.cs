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
            //拖拽
            if (Input.GetMouseButton(0) && go.tag == interactivetag)
            {
                Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
                Vector3 currentPosition = cam.ScreenToWorldPoint(currentScreenSpace) + offset;

                go.transform.position = currentPosition;
                isdrage = true;
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
                player.transform.position = Vector3.Lerp(player.transform.position, currentPosition, temp_t);
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
        }
    }
}
