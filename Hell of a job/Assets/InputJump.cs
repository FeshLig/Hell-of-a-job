using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sp;

    public float jumpforce;
    private bool IsGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void Update()
    {
    }

    void FixedUpdate()
    {
        CheckGrounded();
    }

    private void CheckGrounded()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        IsGrounded = collider.Length > 1;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if(context.performed)
            rb.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
    }
}
