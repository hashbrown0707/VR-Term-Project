using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Valve.VR.Extras;

public class EventTest : MonoBehaviour
{
    public EventTrigger clickpos;

    private void Awake()
    {
        EventTrigger.Entry begin = new EventTrigger.Entry();
        begin.eventID = EventTriggerType.PointerClick;
        begin.callback.AddListener((data) => { OnBegin((PointerEventData)data); });

        clickpos.triggers.Add(begin);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBegin(PointerEventData data)
    {
        Debug.Log(data);
        Debug.Log(data.pointerId);
    }
}
