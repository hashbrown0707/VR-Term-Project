using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawChain : MonoBehaviour
{
    public Color Color { get; set; }
    private LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, this.transform.position);
        line.material.color = Color;
    }
    void OnDestroy()
    {
        Destroy(line);
    }

    public void Set(Vector3 end, Color color, float width = 0.01f)
    {
        //畫線
        line = this.gameObject.AddComponent<LineRenderer>();

        //只有設置了材質 setColor纔有作用
        line.material = new Material(Shader.Find("Unlit/Color"));
        
        line.positionCount = 2;//設置兩點
        Color = color;//設置直線顏色
        line.startWidth = line.endWidth = width;//設置直線寬度

        line.SetPosition(0, this.transform.position);
        line.SetPosition(1, end);
    }
}
