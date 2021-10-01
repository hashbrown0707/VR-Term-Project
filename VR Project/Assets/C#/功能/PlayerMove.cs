using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject this_gameobject;
    public GameObject frontanchor_gameobject;
    public GameObject behindanchor_gameobject;
    public GameObject rightanchor_gameobject;
    public GameObject leftanchor_gameobject;
    public float movespeed = 0.01f;
    public float rotatespeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            this_gameobject.transform.position = Vector3.Lerp(this_gameobject.transform.position, frontanchor_gameobject.transform.position, movespeed * Time.deltaTime);
        }

        if (Input.GetKey("s"))
        {
            this_gameobject.transform.position = Vector3.Lerp(this_gameobject.transform.position, behindanchor_gameobject.transform.position, movespeed * 0.75f * Time.deltaTime);
        }

        if (Input.GetKey("d"))
        {
            this_gameobject.transform.position = Vector3.Lerp(this_gameobject.transform.position, rightanchor_gameobject.transform.position, movespeed * 0.85f * Time.deltaTime);
        }

        if (Input.GetKey("a"))
        {
            this_gameobject.transform.position = Vector3.Lerp(this_gameobject.transform.position, leftanchor_gameobject.transform.position, movespeed * 0.85f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            this_gameobject.transform.Rotate(0, rotatespeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this_gameobject.transform.Rotate(0, -rotatespeed * Time.deltaTime, 0);
        }
    }
}
