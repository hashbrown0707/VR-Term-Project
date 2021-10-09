using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEditor;
using Valve.VR.Extras;

public class ChainMove : MonoBehaviour
{
    public GameObject player;
    public Rigidbody player_rb;
    public float t = 1f;
    public GameObject hand;
    public GameObject shoulder;
    public ChainMove another;
    [SerializeField] KeyCode 發射切換;
    [SerializeField] KeyCode 收繩;
    //private Camera cam;//發射射線的攝像機
    private GameObject go;//射線碰撞的物體
    private bool isdrage = false;
    private RaycastHit hit;

    [SerializeField] LineRenderer line;

    //以下steamVR q2 官方配置
    private SteamVR_Action_Boolean gp = SteamVR_Input.GetBooleanAction("GrabPinch");
    private SteamVR_Action_Boolean tp = SteamVR_Input.GetBooleanAction("Teleport");
    public SteamVR_Input_Sources curhand;

    bool VR_發射切換()
    {
        
        if (gp.GetStateDown(curhand))
        {
            Debug.Log("VR_發射切換");
            return true;
        }
        else
            return false;
    }
    bool VR_收繩()
    {
        if (tp.GetState(curhand))
        {
            Debug.Log("VR_收繩");
            return true;
        }
        else
            return false;
    }
    //以上steamVR q2 官方配置

    // Start is called before the first frame update
    void Start()
    {
        //cam = Camera.main;
        line = gameObject.GetComponent<LineRenderer>();
        line.positionCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, shoulder.transform.position);
        line.SetPosition(1, transform.position);
        if (!isdrage)
        {
            Ray ray = new Ray(hand.transform.position, hand.transform.position - shoulder.transform.position);
            RaycastHit hitInfo;
            line.SetPosition(2, transform.position);
            if (Physics.Raycast(ray, out hitInfo))
            {
                line.SetPosition(2, hitInfo.point);
                go = hitInfo.collider.gameObject;
                hit = hitInfo;
            }
            else
                hit = new RaycastHit();
        }
        if (go != null)
        {
            bool temp = false;
            if ((Input.GetKeyDown(發射切換) || (temp = VR_發射切換())) && !isdrage) 
            {
                if (!temp)
                    hand.GetComponent<HandleSimulator>().enabled = false;
                isdrage = true;
                if (!another.isdrage)
                {
                    player_rb.useGravity = false;
                    player_rb.constraints = RigidbodyConstraints.FreezeAll;
                }
            }
            else if ((Input.GetKeyDown(發射切換) || (temp = VR_發射切換())) && isdrage)
            {
                if (!temp)
                    hand.GetComponent<HandleSimulator>().enabled = true;
                go = null;
                isdrage = false;
                if (!another.isdrage)
                {
                    player_rb.useGravity = true;
                    player_rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
                }
            }
            else if(isdrage)
            {
                if (Input.GetKey(收繩) ||  VR_收繩())
                {
                    Vector3 currentPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    player.transform.position = Vector3.Lerp(player.transform.position, currentPosition, t * Time.deltaTime);
                }
            }
            else
            {
                //結束後，清空物體
                go = null;
            }
        }
    }
}
