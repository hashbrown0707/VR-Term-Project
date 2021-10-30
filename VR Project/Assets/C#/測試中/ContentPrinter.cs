﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentPrinter : MonoBehaviour
{
    public string line;
    public Text content;
    public float printspeed;
    public float printkeep;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        content = canvas.transform.GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set(string s , float f1 = 0.1f, float f2 = 1f)
    {
        line = s;
        printspeed = f1;
        printkeep = f2;
    }

    public void Play()
    {
        content.text = "";
        StartCoroutine(Printing());
    }

    IEnumerator Printing()
    {
        canvas.SetActive(true);
        for (int i = 0; i < line.Length; i++)
        {
            content.text += line[i];
            yield return new WaitForSeconds(printspeed);
        }
        yield return new WaitForSeconds(printkeep);
        canvas.SetActive(false);
    }
}
