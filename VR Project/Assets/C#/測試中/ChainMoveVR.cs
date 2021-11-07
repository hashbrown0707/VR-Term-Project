using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

public class ChainMoveVR : MonoBehaviour
{
    public GameObject player;
    public SteamVR_Behaviour_Pose pose;
    public SteamVR_Action_Boolean gp = SteamVR_Input.GetBooleanAction("GrabPinch");
    public SteamVR_Action_Single sq = SteamVR_Input.GetSingleAction("Squeeze");
    public SteamVR_Action_Boolean a_btn = SteamVR_Input.GetBooleanAction("A_btn");
    public SteamVR_Action_Boolean b_btn = SteamVR_Input.GetBooleanAction("B_btn");

    public bool active = true;
    public Color color;
    public float thickness = 0.002f;
    public Color clickColor = Color.green;
    public GameObject holder;
    public GameObject pointer;
    bool isActive = false;
    public bool addRigidBody = false;
    public Transform reference;

    public bool 鉤鎖鎖定 = false;
    public string 可鎖定tag = "可鎖定";
    public Color 可鎖定color = Color.yellow;
    public Color 已鎖定color = Color.green;
    public Color 收繩color = Color.cyan;
    public Color 繩color = Color.red;
    private float dist;
    private bool bHit;
    private RaycastHit hitinfo;

    public static GameObject keep = null;
    public static int take_cd = 0;
    public ChainMoveVR anotherhand;
    public string 可拿取tag = "可拿取";
    public string 用於移動tag = "用於移動";

    public int chainmode = 0;
    public int modecount = 2;

    private void Start()
    {
        if (pose == null)
            pose = this.GetComponent<SteamVR_Behaviour_Pose>();
        if (pose == null)
            Debug.LogError("No SteamVR_Behaviour_Pose component found on this object", this);
        if (gp == null)
            Debug.LogError("No GrabPinch action has been set on this component.", this);
        if (sq == null)
            Debug.LogError("No Squeeze action has been set on this component.", this);
        if (a_btn == null)
            Debug.LogError("No A_btn action has been set on this component.", this);
        if (b_btn == null)
            Debug.LogError("No B_btn action has been set on this component.", this);

        holder = new GameObject();
        holder.transform.parent = this.transform;
        holder.transform.localPosition = Vector3.zero;
        holder.transform.localRotation = Quaternion.Euler(45f, 0f, 0f);

        pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pointer.transform.parent = holder.transform;
        pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
        pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
        pointer.transform.localRotation = Quaternion.identity;

        BoxCollider collider = pointer.GetComponent<BoxCollider>();
        if (addRigidBody)
        {
            if (collider)
            {
                collider.isTrigger = true;
            }
            Rigidbody rigidBody = pointer.AddComponent<Rigidbody>();
            rigidBody.isKinematic = true;
        }
        else
        {
            if (collider)
            {
                Object.Destroy(collider);
            }
        }
        Material newMaterial = new Material(Shader.Find("Unlit/Color"));
        newMaterial.SetColor("_Color", color);
        pointer.GetComponent<MeshRenderer>().material = newMaterial;
        
        foreach (var hand in FindObjectsOfType<ChainMoveVR>())
            if (hand != this)
                anotherhand = hand;

    }

    private void Update()
    {
        if (!isActive)
        {
            isActive = true;
            this.transform.GetChild(0).gameObject.SetActive(true);
        }

        dist = 100f;

        Ray ray = new Ray(transform.position, transform.forward-transform.up);
        bHit = false;

        if (!鉤鎖鎖定)
        {
            bHit = Physics.Raycast(ray, out hitinfo);
        }

        if (bHit && hitinfo.distance < 100f)
        {
            dist = hitinfo.distance;
        }

        if (bHit && hitinfo.collider.gameObject.TryGetComponent(out ObjectItem oi))
        {
            oi.OnPlayerLookAt();
        }

        if (a_btn.GetStateDown(pose.inputSource))
            chainmode = (chainmode + 1) % modecount;

        /*if (chainmode == 0)
            Move();
        else if (chainmode == 1)*/
            Catch();

    }
    void Move()
    {
        if ((bHit || 鉤鎖鎖定) && gp.GetStateDown(pose.inputSource) && !hitinfo.collider.gameObject.CompareTag(可拿取tag))
        {
            鉤鎖鎖定 = !鉤鎖鎖定;
            if (鉤鎖鎖定)
            {
                DrawChain dc = this.gameObject.AddComponent<DrawChain>();
                dc.Set(hitinfo.point, 已鎖定color);
            }
            else if (TryGetComponent<DrawChain>(out DrawChain dc))
                Destroy(dc);
        }

        if (鉤鎖鎖定 && sq.axis != 0)
        {
            Vector3 currentPosition = new Vector3(hitinfo.point.x, hitinfo.point.y, hitinfo.point.z);
            player.transform.position = Vector3.Lerp(player.transform.position, currentPosition, sq.axis * Time.deltaTime);
            pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
            pointer.GetComponent<MeshRenderer>().material.color = 收繩color;
        }
        else if (鉤鎖鎖定)
        {
            pointer.transform.localScale = new Vector3(thickness, thickness, dist);
            pointer.GetComponent<MeshRenderer>().material.color = 已鎖定color;
            gameObject.GetComponent<DrawChain>().Color = 繩color;
        }
        else if (bHit)
        {
            pointer.transform.localScale = new Vector3(thickness, thickness, dist);
            pointer.GetComponent<MeshRenderer>().material.color = 可鎖定color;
        }
        else if (gp != null && gp.GetState(pose.inputSource))
        {
            pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
            pointer.GetComponent<MeshRenderer>().material.color = clickColor;
        }
        else
        {
            pointer.transform.localScale = new Vector3(thickness, thickness, dist);
            pointer.GetComponent<MeshRenderer>().material.color = color;
        }
        pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);
    }
    void Catch()
    {
        var temp_gpsd = gp.GetStateDown(pose.inputSource);
        GameObject temp_hitgo = null;
        if (bHit)
            temp_hitgo = hitinfo.collider.gameObject;

        // 對UI按下互動鍵"f"
        if (bHit && temp_gpsd && temp_hitgo.TryGetComponent(out VR_UIBtn btn))
        {
            btn.OnHit();
        }
        // 按下互動鍵"f"
        if (bHit && temp_gpsd && temp_hitgo == keep && temp_hitgo.TryGetComponent(out ObjectItem objectItem1))
        {
            objectItem1.Keydown();
        }
        else if (bHit && temp_gpsd && temp_hitgo.TryGetComponent(out Door door))
        {
            door.Keydown();
        }
        //短按 拿到面前
        else if (bHit && temp_gpsd && keep == null && take_cd == 0 && temp_hitgo.CompareTag(可拿取tag))
        {
            if (temp_hitgo.TryGetComponent(out ObjectItem objectItem))
            {
                objectItem.SetState(2);
                keep = temp_hitgo;
                take_cd = 60;
            }
        }
        //短按 放下
        else if (temp_gpsd && keep != null)
        {
            if (keep.TryGetComponent(out ObjectItem objectItem))
            {
                objectItem.SetState(1);
                keep = null;
                take_cd = 0;
            }
        }
        //鎖鏈移動
        else if (bHit && gp.GetState(pose.inputSource) && temp_hitgo.CompareTag(用於移動tag) && sq.axis != 0)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, hitinfo.point, sq.axis * Time.deltaTime);
            pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
            pointer.GetComponent<MeshRenderer>().material.color = 收繩color;
        }
        else if(bHit && temp_hitgo.TryGetComponent(out VR_UIBtn _))
        {
            pointer.transform.localScale = new Vector3(thickness, thickness, dist);
            pointer.GetComponent<MeshRenderer>().material.color = Color.magenta;
        }
        else if (bHit && temp_hitgo.TryGetComponent(out Door _))
        {
            pointer.transform.localScale = new Vector3(thickness, thickness, dist);
            pointer.GetComponent<MeshRenderer>().material.color = Color.cyan;
        }
        else if(bHit && temp_hitgo.CompareTag(可拿取tag))
        {
            pointer.transform.localScale = new Vector3(thickness, thickness, dist);
            pointer.GetComponent<MeshRenderer>().material.color = Color.cyan;
        }
        else if (bHit && temp_hitgo.CompareTag(用於移動tag))
        {
            pointer.transform.localScale = new Vector3(thickness, thickness, dist);
            pointer.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else
        {
            pointer.transform.localScale = new Vector3(thickness, thickness, dist);
            pointer.GetComponent<MeshRenderer>().material.color = color;
        }
        take_cd = (take_cd == 0) ? 0 : (take_cd - 1);
        pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);
    }
}
