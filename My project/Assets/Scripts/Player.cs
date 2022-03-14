using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour


{

    public float Speed;
    public float JumpForce;

    public bool IsJumping;
    public bool IsDoubleJumping;

    private Rigidbody2D rig;
    private Animator anim;

    public CircleCollider2D circleCollider2D;
    public BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;

        if(Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("IsMove", true);
            transform.eulerAngles = new Vector3(0f,0f,0f);
        }

        if(Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("IsMove", true);
            transform.eulerAngles = new Vector3(0f,180,0f);
        }

        if(Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("IsMove", false);
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(!IsJumping)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                IsDoubleJumping = false;
                anim.SetBool("IsJump", true);
            }
            else
            {
                if(!IsDoubleJumping)
                {
                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    IsDoubleJumping = true;
                    anim.SetBool("IsJump", false);
                    anim.SetBool("IsDoubleJump", true);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            IsJumping = false;
            anim.SetBool("IsJump", false);
            anim.SetBool("IsDoubleJump", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            IsJumping = true;
            anim.SetBool("IsMove", false);
        }
    }
}
