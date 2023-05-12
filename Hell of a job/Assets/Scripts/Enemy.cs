using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ссылка на объект игрока
    protected GameObject player;

    // включено ли агрессивное поведение по отношению к игроку
    protected bool isAggressive = false;

    [Header("Common Enemy Properties")]

    // расстояние, на котором враг замечает игрока
    [SerializeField] protected float playerDetectionDistance = 15f;

    // использовать ли триггерный коллайдер для определения столкновения с игроком
    [SerializeField] bool useTriggerCollision = false;
    // интервал между нанесениями урона, если игрок находится внутри врага
    [SerializeField] float stayInsideDamageTimeInterval = 1f;
    // переменная для отсчёта времени до нанесения урона, если игрок находится внутри врага
    float stayInsideT;

    // наносимый урон от столкновения
    [SerializeField] protected float collisionDamage = 10f;

    protected void Start()
    {
        player = FindObjectOfType<Person>().gameObject;
    }

    protected void Update()
    {
        isAggressive = Vector2.Distance((Vector2)transform.position, (Vector2)player.transform.position) <= playerDetectionDistance;
    }

    // нанести игроку урон от столкновения
    void ApplyCollisionDamage()
    {
        player.GetComponent<PlayerHealthManager>().TakeDamage(collisionDamage);
        player.GetComponent<Knockback>().ApplyKnockback(transform);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (!useTriggerCollision)
            return;
        
        if (col.CompareTag("Player"))
            ApplyCollisionDamage();
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (!useTriggerCollision)
            return;
        
        if (col.CompareTag("Player"))
        {
            stayInsideT += Time.deltaTime;
            
            if (stayInsideT >= stayInsideDamageTimeInterval)
            {
                stayInsideT = 0;
                ApplyCollisionDamage();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (!useTriggerCollision)
            return;

        if (col.CompareTag("Player"))
            stayInsideT = 0;
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (useTriggerCollision)
            return;
        
        if (col.collider.CompareTag("Player"))
            ApplyCollisionDamage();
    }
}
