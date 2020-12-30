using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl: MonoBehaviour
{
    public GameObject camera_gameobject;
    public float rotatespeed = 0.5f;
    public GameObject light_gameobject;
    public int light_cd = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && light_cd == 0)
        {
            light_gameobject.GetComponent<Light>().enabled = light_gameobject.GetComponent<Light>().enabled ? false : true;
            light_cd = 60;
        }
        light_cd = (light_cd == 0) ? 0 : light_cd - 1;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            camera_gameobject.transform.Rotate(-rotatespeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            camera_gameobject.transform.Rotate(rotatespeed, 0, 0);
        }
    }
}
