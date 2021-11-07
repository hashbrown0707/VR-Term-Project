using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RotateKeepVR : MonoBehaviour
{
    public GameObject rotate_hand;
    public SteamVR_Behaviour_Pose pose;
    public SteamVR_Action_Boolean gg = SteamVR_Input.GetBooleanAction("GrabGrip");
    public GameObject player;
    public float dist;
    private GameObject keep_slot;
    public GameObject max;
    public GameObject min;
    public float zoom_trigger = 1f;
    public Vector3 origin;
    public Vector3 rotate;
    
    // Start is called before the first frame update
    void Start()
    {
        keep_slot = gameObject;
        if (pose == null)
            pose = rotate_hand.transform.GetComponent<SteamVR_Behaviour_Pose>();
        if (pose == null)
            Debug.LogError("No SteamVR_Behaviour_Pose component found on this object", this);
        if (gg == null)
            Debug.LogError("No GrabGrip action has been set on this component.", this);
    }

    // Update is called once per frame
    void Update()
    {
        if (gg.GetStateDown(pose.inputSource))
        {
            rotate = rotate_hand.transform.rotation.eulerAngles;
            origin = keep_slot.transform.rotation.eulerAngles;
            dist = Vector3.Distance(rotate_hand.transform.position, player.transform.position);
        }
        else if(gg.GetState(pose.inputSource))
        {
            var temp_rotate = rotate_hand.transform.rotation.eulerAngles - rotate;
            keep_slot.transform.rotation = Quaternion.Euler(origin + temp_rotate);

            

            var temp_dist = Vector3.Distance(rotate_hand.transform.position, player.transform.position);
            if ((temp_dist - dist - zoom_trigger) > 0 && temp_dist > dist + zoom_trigger)
                keep_slot.transform.position = Vector3.Lerp(keep_slot.transform.position, min.transform.position, (temp_dist - dist - zoom_trigger) * Time.deltaTime);
            else if ((dist - temp_dist - zoom_trigger) > 0 && dist > temp_dist + zoom_trigger) 
                keep_slot.transform.position = Vector3.Lerp(keep_slot.transform.position, max.transform.position, (dist - temp_dist - zoom_trigger) * Time.deltaTime);
        }
    }

    /// <summary>
    /// 重置keep格的旋轉與放大縮小
    /// </summary>
    public void ResetRotateAndPos()
    {
        keep_slot.transform.position = min.transform.position;
        keep_slot.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
