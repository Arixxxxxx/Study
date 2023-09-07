using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] BoxCollider2D LegCollider;
    [SerializeField] Rigidbody2D Rb;

    [SerializeField] private bool isGround;
    [SerializeField] private float GroundRatio;
    [SerializeField] private float MoveSpeed;

    private Animator Ani;
    private float gravityY = 9.81f;
    private float veloY = 0f;
    private float MaxVeloY = -10f;
    private bool isJump;
    [SerializeField] private float JumpPower;
    SpriteRenderer Sr;

    Vector2 moveDir;

    private void Awake()
    {
        Ani = GetComponent<Animator>();
        Sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        F_CheackGround();
        F_CheackGrivity();
        F_PlayerMove();
        F_DoAni();
        F_filx();

    }

    private void F_PlayerMove()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        Rb.velocity = new Vector2(moveDir.x * MoveSpeed, Rb.velocity.y);
    }
    private void F_CheackGrivity()
    {
        if (!isGround)
        {
            veloY -= gravityY * Time.deltaTime;
            if (veloY < MaxVeloY)
            {
                veloY = MaxVeloY;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Space) && !isJump)
            {
                veloY = JumpPower;
                isJump = true;
            }
            else
            {
                veloY = 0;
                isJump = false;
            }
        }
        Rb.velocity = new Vector2(Rb.velocity.x, veloY);
    }
    private void F_CheackGround()
    {
        bool beforeGround = isGround;
        RaycastHit2D cheakdown = Physics2D.BoxCast(LegCollider.bounds.center, LegCollider.bounds.size, 0f, Vector3.down, GroundRatio, LayerMask.GetMask("Ground"));

        if (cheakdown)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }

    private void F_filx()
    {
        if (moveDir.x < 0 && transform.localScale.x != 1.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (moveDir.x > 0 && transform.localScale.x != -1.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
    }
    private void F_DoAni()
    {
        Ani.SetInteger("VelocityX", (int)moveDir.x);
    }

}
