using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Person : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    private SpriteRenderer sp;


    [SerializeField] int MoveInput = 0;
    [SerializeField] int direction = 0;
    [SerializeField] float dashtime = 0f;
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
        if (IsGrounded && Input.GetButtonDown("Jump"))
            Jump();
        Dash();
    }

    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxisRaw("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
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
            if (MoveInput < 0)
            {
                direction = 1;
            }
            else if (MoveInput == 0)
            {
                direction = sp.flipX ? 1 : 2;
            }
            else
            {
                direction = 2;
            }

            dashtime = startdash;
        }

        if (direction != 0)
        {
            if (dashtime <= 0)
            {
                direction = 0;
                dashtime = startdash;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashtime -= Time.deltaTime;
                rb.velocity = direction == 2 ? Vector2.right * dashspeed : Vector2.left * dashspeed;
            }
        }

    }
}