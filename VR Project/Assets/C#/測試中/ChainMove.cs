using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainMove : MonoBehaviour
{
    public GameObject player;
    public Rigidbody player_rb;
    public float t = 1f;
    public GameObject hand;
    public GameObject shoulder;
    public ChainMove another;
    [SerializeField] KeyCode 發射切換;
    [SerializeField] KeyCode 收繩;
    //private Camera cam;//發射射線的攝像機
    private GameObject go;//射線碰撞的物體
    private bool isdrage = false;
    private RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {
        //cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isdrage)
        {
            Ray ray = new Ray(hand.transform.position, hand.transform.position - shoulder.transform.position);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
                go = hitInfo.collider.gameObject;
                hit = hitInfo;
            }
            else
                hit = new RaycastHit();
        }

        if (go != null)
        {
            if (Input.GetKeyDown(發射切換) && !isdrage)
            {
                hand.GetComponent<HandleSimulator>().enabled = false;
                isdrage = true;
                if (!another.isdrage)
                {
                    player_rb.useGravity = false;
                    player_rb.constraints = RigidbodyConstraints.FreezeAll;
                }
            }
            else if (Input.GetKeyDown(發射切換) && isdrage)
            {
                hand.GetComponent<HandleSimulator>().enabled = true;
                go = null;
                isdrage = false;
                if (!another.isdrage)
                {
                    player_rb.useGravity = true;
                    player_rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
                }
            }
            else if(isdrage)
            {
                Debug.DrawLine(this.transform.position, hit.point, Color.green);
                if (Input.GetKey(收繩))
                {
                    Vector3 currentPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    player.transform.position = Vector3.Lerp(player.transform.position, currentPosition, t * Time.deltaTime);
                }
            }
            else
            {
                //結束後，清空物體
                go = null;
            }
        }
    }
}
