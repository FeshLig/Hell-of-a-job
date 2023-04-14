using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Person : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer sp;

    [SerializeField] LayerMask groundLayerMask;

    int MoveInput = 0;
    int direction = 0;
    float dashtime = 0f;
    [SerializeField] float startdash = 0.25f;
    [SerializeField] float dashspeed = 20f;
    public float speed;
    public float jumpforce;
    private bool IsGrounded = false;




    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        CheckMove();
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal"))
            Run();
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        if (IsGrounded && Input.GetButtonDown("Jump"))
            Jump();
        Dash();
    }

    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        sp.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
    }


    private void CheckGrounded()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        IsGrounded = collider.Length > 1;
    }

    private void CheckMove()
    {
        var dir = transform.right * Input.GetAxisRaw("Horizontal");
        MoveInput = dir.x < 0 ? -1 : dir.x > 0 ? 1 : 0;
    }

    private void Dash()
    {
        if (Input.GetButtonDown("Dash"))
        {
            direction = (MoveInput != 0) ? MoveInput : (sp.flipX ? -1 : 1);
            dashtime = startdash;
        }

        if (direction == 0)
        {
            return;
        }
        if(dashtime <= 0)
        {
            direction = 0;
            dashtime = startdash;
        }
        else
        {
            dashtime -= Time.deltaTime;
            rb.velocity = new Vector2(rb.velocity.x + direction * dashspeed, 0f);
        }
    }
}