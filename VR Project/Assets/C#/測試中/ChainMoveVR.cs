using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

public class ChainMoveVR : MonoBehaviour
{
    public GameObject player;
    public SteamVR_Behaviour_Pose pose;

    //public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.__actions_default_in_InteractUI;
    public SteamVR_Action_Boolean gp = SteamVR_Input.GetBooleanAction("GrabPinch");
    public SteamVR_Action_Boolean tp = SteamVR_Input.GetBooleanAction("Teleport");

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
    private RaycastHit hitinfo;

    private void Start()
    {
        if (pose == null)
            pose = this.GetComponent<SteamVR_Behaviour_Pose>();
        if (pose == null)
            Debug.LogError("No SteamVR_Behaviour_Pose component found on this object", this);

        if (gp == null)
            Debug.LogError("No GrabPinch action has been set on this component.", this);

        if (tp == null)
            Debug.LogError("No Teleport action has been set on this component.", this);


        holder = new GameObject();
        holder.transform.parent = this.transform;
        holder.transform.localPosition = Vector3.zero;
        holder.transform.localRotation = Quaternion.identity;

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
    }

    private void Update()
    {
        if (!isActive)
        {
            isActive = true;
            this.transform.GetChild(0).gameObject.SetActive(true);
        }

        float dist = 100f;

        Ray ray = new Ray(transform.position, transform.forward);
        bool bHit = false;
        string hitTag = null;

        if (!鉤鎖鎖定)
        {
            bHit = Physics.Raycast(ray, out hitinfo);
        }

        if (bHit && hitinfo.distance < 100f)
        {
            dist = hitinfo.distance;
            hitTag = hitinfo.transform.gameObject.tag;
        }
        if ((bHit || 鉤鎖鎖定) && gp.GetStateDown(pose.inputSource)) //待加入 可鎖定tag
        {
            鉤鎖鎖定 = !鉤鎖鎖定;
            if(鉤鎖鎖定)
            {
                DrawChain dc = this.gameObject.AddComponent<DrawChain>();
                dc.Set(hitinfo.point, 已鎖定color);
            }
            else if(TryGetComponent<DrawChain>(out DrawChain dc))
                    Destroy(dc);
        }


        if (鉤鎖鎖定 && tp.GetState(pose.inputSource))
        {
            Vector3 currentPosition = new Vector3(hitinfo.point.x, hitinfo.point.y, hitinfo.point.z);
            player.transform.position = Vector3.Lerp(player.transform.position, currentPosition, Time.deltaTime);
            pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
            pointer.GetComponent<MeshRenderer>().material.color = 收繩color;
            gameObject.GetComponent<DrawChain>().Color = 收繩color;
        }
        else if (鉤鎖鎖定)
        {
            pointer.transform.localScale = new Vector3(thickness, thickness, dist);
            pointer.GetComponent<MeshRenderer>().material.color = 已鎖定color;
            gameObject.GetComponent<DrawChain>().Color = 已鎖定color;
        }
        else if (bHit && hitTag == 可鎖定tag) 
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
}
