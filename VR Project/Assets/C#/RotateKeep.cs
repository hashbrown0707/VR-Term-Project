using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateKeep : MonoBehaviour
{
    public float rotatespeed = 0.5f;
    public float movespeed = 0.1f;
    public GameObject max;
    public GameObject min;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("i"))
        {
            this.gameObject.transform.Rotate(0, 0, rotatespeed);
        }
        if (Input.GetKey("k"))
        {
            this.gameObject.transform.Rotate(0, 0, -rotatespeed);
        }

        if (Input.GetKey("j"))
        {
            this.gameObject.transform.Rotate(rotatespeed, 0, 0);
        }
        if (Input.GetKey("l"))
        {
            this.gameObject.transform.Rotate(-rotatespeed, 0, 0);
        }

        if (Input.GetKey("u"))
        {
            this.gameObject.transform.Rotate(0, rotatespeed, 0);
        }
        if (Input.GetKey("o"))
        {
            this.gameObject.transform.Rotate(0, -rotatespeed, 0);
        }

        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, max.transform.position, movespeed);
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, min.transform.position, movespeed);
        }
    }

    public void ResetRotateAndPos()
    {
        this.gameObject.transform.position = min.transform.position;
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
