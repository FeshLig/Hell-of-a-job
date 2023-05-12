using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    // ссылка на спрайт
    protected SpriteRenderer spriteRenderer;
    // ссылка на аниматор
    protected Animator animator;

    // максимальное количество здоровья
    [SerializeField] protected float maxHealth = 100f;
    // текущее количество здоровья
    [SerializeField] protected float currentHealth = 100f;

    // множитель для получаемого урона
    [SerializeField] protected float damageMultiplier = 1f;

    protected void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //animator = GetComponent<Animator>();
        TryGetComponent<Animator>(out animator);
        currentHealth = maxHealth;
    }

    // получить урон
    public void TakeDamage(float amount)
    {
        currentHealth -= amount * damageMultiplier;
        StartCoroutine(MakeSpriteRed());

        if (currentHealth <= 0)
            Die();
    }

    IEnumerator MakeSpriteRed()
    {
        spriteRenderer.color = new Color
        (
            spriteRenderer.color.r,
            spriteRenderer.color.g / 2f,
            spriteRenderer.color.b / 2f,
            spriteRenderer.color.a
        );

        yield return new WaitForSeconds(0.25f);
        
        spriteRenderer.color = new Color
        (
            spriteRenderer.color.r,
            spriteRenderer.color.g * 2f,
            spriteRenderer.color.b * 2f,
            spriteRenderer.color.a
        );
    }

    // восстановить здоровье
    public void Heal(float amount)
    {
        currentHealth += amount;
    }

    // умереть
    protected void Die()
    {
        if (animator != null)
            animator.SetBool("isDead", true);
        
        GetComponent<IMoving>().DisableCompletely();
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Collider2D>().enabled = false;
    }

    // увеличить получаемый урон на %
    public void IncreaseDamageMultiplier(float percentage)
    {
        damageMultiplier += percentage * 0.01f;
    }

    // временно увеличить получаемый урон на %
    public void IncreaseDamageMultiplier(float percentage, float seconds)
    {
        StartCoroutine(TemporarilyIncreaseDamageMultiplier(percentage, seconds));
    }

    // корутина для временного увеличения получаемого урона на %
    protected IEnumerator TemporarilyIncreaseDamageMultiplier(float percentage, float seconds)
    {
        damageMultiplier += percentage * 0.01f;
        yield return new WaitForSeconds(seconds);
        damageMultiplier -= percentage * 0.01f;
    }

    // уменьшить получаемый урон на %
    public void DecreaseDamageMultiplier(float percentage)
    {
        damageMultiplier -= percentage * 0.01f;
    }

    // временно уменьшить получаемый урон на %
    public void DecreaseDamageMultiplier(float percentage, float seconds)
    {
        StartCoroutine(TemporarilyDecreaseDamageMultiplier(percentage, seconds));
    }

    // корутина для временного уменьшения получаемого урона на %
    protected IEnumerator TemporarilyDecreaseDamageMultiplier(float percentage, float seconds)
    {
        damageMultiplier -= percentage * 0.01f;
        yield return new WaitForSeconds(seconds);
        damageMultiplier += percentage * 0.01f;
    }
}
