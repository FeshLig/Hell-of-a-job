using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    Inventory inventory;

    [SerializeField] bool usePointAndClick = false;

    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    float attackDamage;

    [SerializeField] LayerMask enemyLayerMask;
    
    void Start()
    {
        TryGetComponent<Animator>(out animator);
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        if (inventory.IsEmpty)
            return;

        if (InputManager.LightAttackWasPressedThisFrame)
        {
            Attack(inventory.CurrentWeapon.lightAttackDamage);
            animator.SetTrigger("Attack");
        }
        else if (InputManager.HeavyAttackWasPressedThisFrame)
        {
            Attack(inventory.CurrentWeapon.heavyAttackDamage);
            animator.SetTrigger("Attack2");
        }        
        else if (InputManager.SpecialAttackWasPressedThisFrame)
            Attack(inventory.CurrentWeapon.specialAttackDamage);
    }

    void Attack(float damage)
    {
        if (usePointAndClick)
            attackPoint.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayerMask);
        
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<HealthManager>().TakeDamage(damage);
            enemy.GetComponent<Knockback>().ApplyKnockback(transform);
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
