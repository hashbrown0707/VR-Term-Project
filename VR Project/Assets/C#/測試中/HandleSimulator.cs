using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleSimulator : MonoBehaviour
{
    public float movespeed = 10f;
    /// <summary>前 後 左 右 上 下</summary>
    [Tooltip("0前 1後 2左 3右 4上 5下")]
    [SerializeField] KeyCode[] 方向 = new KeyCode[6];
    [SerializeField] KeyCode reset;
    /// <summary>前 後 左 右 上 下</summary>
    Vector3[] 錨 = new Vector3[6];
    Vector3 原點;
    // Start is called before the first frame update
    void Start()
    {
        原點 = this.gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        回到原點();
    }
    void 錨Update()
    {
        錨[0] = this.gameObject.transform.localPosition + new Vector3(1, 0, 0);
        錨[1] = this.gameObject.transform.localPosition + new Vector3(-1, 0, 0);
        錨[2] = this.gameObject.transform.localPosition + new Vector3(0, 0, 1);
        錨[3] = this.gameObject.transform.localPosition + new Vector3(0, 0, -1);
        錨[4] = this.gameObject.transform.localPosition + new Vector3(0, 1, 0);
        錨[5] = this.gameObject.transform.localPosition + new Vector3(0, -1, 0);
    }
    void move()
    {
        for (int i = 0; i < 6; i++)
            if (Input.GetKey(方向[i]))
            {
                錨Update();
                this.gameObject.transform.localPosition = Vector3.Lerp(this.gameObject.transform.localPosition, 錨[i], movespeed * Time.deltaTime);
            }
    }
    void 回到原點()
    {
        if (Input.GetKeyDown(reset))
            this.gameObject.transform.localPosition = 原點;
    }
}
