using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordSYS : MonoBehaviour
{
    public GameObject[] ps_slot;
    public Sprite[] num_list;
    public Sprite num_none;
    public GameObject player;
    public GameObject keep_slot;
    public GameObject main_camera;
    public GameObject lock_gameObject;
    public bool pwlock = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openpasswordtable()
    {
        for (int i = 0; i < ps_slot.Length; i++)
        {
            ps_slot[i].GetComponent<Image>().sprite = num_none;
        }
        if (FindObjectOfType<PlayerMove>())
            player.GetComponent<PlayerMove>().enabled = false;
        if (FindObjectOfType<mousetake>())
            player.GetComponent<mousetake>().enabled = false;
        if (FindObjectOfType<RotateKeep>())
            keep_slot.GetComponent<RotateKeep>().enabled = false;
        if (FindObjectOfType<CameraControl>())
            main_camera.GetComponent<CameraControl>().enabled = false;
        keep_slot.SetActive(false);

        this.gameObject.SetActive(true);
    }

    public void numbtn(int num)
    {
        for (int i = ps_slot.Length - 1; i > 0; i--) 
        {
            ps_slot[i].GetComponent<Image>().sprite = ps_slot[i - 1].GetComponent<Image>().sprite;
        }
        ps_slot[0].GetComponent<Image>().sprite = num_list[num];

    }

    public void exitbtn()
    {
        this.gameObject.SetActive(false);

        if (FindObjectOfType<PlayerMove>())
            player.GetComponent<PlayerMove>().enabled = true;
        if (FindObjectOfType<mousetake>())
            player.GetComponent<mousetake>().enabled = true;
        if (FindObjectOfType<RotateKeep>())
            keep_slot.GetComponent<RotateKeep>().enabled = true;
        if (FindObjectOfType<CameraControl>())
            main_camera.GetComponent<CameraControl>().enabled = true;
        keep_slot.SetActive(true);

        if (lock_gameObject.TryGetComponent<OwlPhone>(out var owlphone))
            owlphone.SetState(2);
    }

    public void backbtn()
    {
        for (int i = 0; i < ps_slot.Length-1; i++)
        {
            ps_slot[i].GetComponent<Image>().sprite = ps_slot[i + 1].GetComponent<Image>().sprite;
        }
        ps_slot[ps_slot.Length - 1].GetComponent<Image>().sprite = num_none;
    }

    public void surebtn()
    {
        if (ps_slot[0].GetComponent<Image>().sprite == num_list[4] &&
            ps_slot[1].GetComponent<Image>().sprite == num_list[3] &&
            ps_slot[2].GetComponent<Image>().sprite == num_list[2] &&
            ps_slot[3].GetComponent<Image>().sprite == num_list[1])
        {
            Debug.Log("正確");
            ImgManager.op_port.SwitchImgManager(0, 2);
            pwlock = false;
            exitbtn();
        }
        else
        {
            Debug.Log("password error");
            pwlock = true;
        }
    }
}
