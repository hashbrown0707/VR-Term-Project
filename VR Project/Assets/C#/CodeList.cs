using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeList : MonoBehaviour
{
    public bool update = false;
    public MonoBehaviour[] list;

    void Start()
    {
        list = Object.FindObjectsOfType<MonoBehaviour>();
    }

    void Update()
    {
        if (update)
        {
            update = false;
            list = Object.FindObjectsOfType<MonoBehaviour>();
        }
    }
}
