using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    // ссылка на Rigidbody
    Rigidbody2D rb;
    // ссылка на спрайт
    SpriteRenderer sp;

    // маска слоёв коллизии для проверки, приземлён ли игрок
    [SerializeField] LayerMask groundLayerMask;

    // целевое направление движения
    int MoveInput = 0;
    // направление дэша
    int direction = 0;

    // длительность дэша в секундах
    [SerializeField] float startdash = 0.25f;
    // скорость дэша в юнит/сек
    [SerializeField] float dashspeed = 20f;
    // переменная для отсчёта времени дэша
    float dashtime = 0f;

    // скорость бега в юнит/сек
    [SerializeField] float speed;
    // сила прыжка
    [SerializeField] float jumpforce;

    // приземлён ли игрок
    bool IsGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponentInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        CheckGrounded();
        CheckMove();
    }

    void Update()
    {
        if (Input.GetButton("Horizontal"))
            Run();
        else
            rb.velocity = new Vector2(0f, rb.velocity.y);

        if (IsGrounded && Input.GetButtonDown("Jump"))
            Jump();
        
        Dash();
    }

    // проверка, приземлён ли игрок
    private void CheckGrounded()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f, groundLayerMask);
        IsGrounded = collider.Length > 1;
    }

    // проверка целевого направления движения
    private void CheckMove()
    {
        Vector2 dir = transform.right * Input.GetAxisRaw("Horizontal");
        MoveInput = dir.x < 0 ? -1 : dir.x > 0 ? 1 : 0;
    }

    // бег
    private void Run()
    {
        Vector2 dir = transform.right * Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        sp.flipX = dir.x < 0.0f;
    }

    // прыжок
    private void Jump()
    {
        rb.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
    }

    // дэш
    private void Dash()
    {
        if (Input.GetButtonDown("Dash"))
        {
            direction = (MoveInput != 0) ? MoveInput : (sp.flipX ? -1 : 1);
            dashtime = startdash;
        }

        if (direction == 0)
            return;

        if (dashtime <= 0)
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