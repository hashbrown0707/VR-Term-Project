using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateKeep : MonoBehaviour
{
    public float rotatespeed = 90f;
    public float movespeed = 1f;
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
            this.gameObject.transform.Rotate(0, 0, rotatespeed * Time.deltaTime);
        }
        if (Input.GetKey("k"))
        {
            this.gameObject.transform.Rotate(0, 0, -rotatespeed * Time.deltaTime);
        }

        if (Input.GetKey("j"))
        {
            this.gameObject.transform.Rotate(rotatespeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("l"))
        {
            this.gameObject.transform.Rotate(-rotatespeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("u"))
        {
            this.gameObject.transform.Rotate(0, rotatespeed * Time.deltaTime, 0);
        }
        if (Input.GetKey("o"))
        {
            this.gameObject.transform.Rotate(0, -rotatespeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, max.transform.position, movespeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, min.transform.position, movespeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 重置keep格的旋轉與放大縮小
    /// </summary>
    public void ResetRotateAndPos()
    {
        gameObject.transform.position = min.transform.position;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
