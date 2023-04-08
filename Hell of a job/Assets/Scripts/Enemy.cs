using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ссылка на менеджер здоровья игрока
    HealthManager playerHealthManager;
    
    // использовать ли триггерный коллайдер для определения столкновения с игроком
    [SerializeField] bool useTriggerCollision = false;

    // наносимый урон от столкновения
    [SerializeField] float collisionDamage = 25f;

    void Start()
    {
        playerHealthManager = FindObjectOfType<Person>().GetComponent<HealthManager>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (!useTriggerCollision)
            return;
        
        if (col.CompareTag("Player"))
            playerHealthManager.TakeDamage(collisionDamage);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (useTriggerCollision)
            return;
        
        if (col.collider.CompareTag("Player"))
            playerHealthManager.TakeDamage(collisionDamage);
    }
}
