using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour, IMoving
{
    // ссылка на Rigidbody
    Rigidbody2D rb;
    // ссылка на спрайт
    SpriteRenderer sp;
    // ссылка на коллайдер
    BoxCollider2D col;

    // включено ли движение
    bool enableMovement = true;

    // смещение коллайдера для проверки, приземлён ли игрок
    [SerializeField] float groundCheckYOffset = -0.03f;
    // на сколько уменьшить ширину коллайдера для проверки, приземлён ли игрок
    // (необходимо для предотвращения прыжков от стен)
    [SerializeField] float groundCheckShrinkColliderWidthBy = 0.02f;
    // маска слоя коллизий для проверки, приземлён ли игрок
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
        col = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        CheckGrounded();
        CheckMove();
    }

    void Update()
    {
        rb.WakeUp();

        if (!enableMovement)
            return;
        
        if (Input.GetButton("Horizontal"))
            Run();
        else if (Input.GetButtonUp("Horizontal") || IsGrounded)
            rb.velocity = new Vector2(0f, rb.velocity.y);

        if (IsGrounded && Input.GetButtonDown("Jump"))
            Jump();

        Dash();
    }

    // проверка, приземлён ли игрок
    private void CheckGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll
        (
            point: col.bounds.center + new Vector3(0f, groundCheckYOffset, 0f),
            size: new Vector2(col.size.x - groundCheckShrinkColliderWidthBy, col.size.y),
            angle: 0f,
            layerMask: groundLayerMask
        );

        IsGrounded = colliders.Length > 0;
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
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        else
        {
            dashtime -= Time.deltaTime;
            rb.velocity = new Vector2(direction * dashspeed, 0f);
        }
    }

    // включить движение (реализация интерфейса IMoving)
    public void EnableMovement()
    {
        enableMovement = true;
    }

    // выключить движение (реализация интерфейса IMoving)
    public void DisableMovement()
    {
        enableMovement = false;
        dashtime = 0f;
        rb.velocity = new Vector2(0f, rb.velocity.y);
    }
}
