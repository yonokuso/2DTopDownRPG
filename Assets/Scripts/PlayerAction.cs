using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float Speed;
    public GameManager manager;

    float h;
    float v;
    bool isHorizonMove;
    Vector3 dirVec;
    GameObject scanObject;

    Rigidbody2D rigid;
    Animator anim;


    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move Value
        // 상태 변수를 이용하여 플레이어 이동을 제한
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal"); 
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        // Check button Down & Up
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");

        // Check Horizontal Move
        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = h != 0;

        // Animation
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("IsChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("IsChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
            anim.SetBool("IsChange", false);

        // Direction
        if (vDown && v == 1)
            dirVec = Vector3.up;
        if (vDown && v == -1)
            dirVec = Vector3.down;
        if (hDown && h == 1)
            dirVec = Vector3.right;
        if (hDown && h == -1)
            dirVec = Vector3.left;

        //Scan object
        if (Input.GetButtonDown("Jump") && scanObject != null)
            manager.Action(scanObject);
    }


    void FixedUpdate()
    {
        // Move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;


        // Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;
    }
}
