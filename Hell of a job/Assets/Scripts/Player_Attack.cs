using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Attack : MonoBehaviour
{

    Animator animator;

    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;

    [SerializeField] LayerMask enemyLayerMask;
    [SerializeField] float lightAttackDamage;
    [SerializeField] float heavyAttackDamage;
    
    void Start()
    {
        TryGetComponent<Animator>(out animator);
    }

    void Update()
    {
        if (InputManager.LightAttackWasPressedThisFrame)
            Attack(false);
        if (InputManager.HeavyAttackWasPressedThisFrame)
            Attack(true);
    }

    void Attack(bool isHeavyAttack)
    {
        //��������
        animator.SetTrigger("Attack");
        //����������� ������
        attackPoint.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        float attackDamage = isHeavyAttack ? heavyAttackDamage : lightAttackDamage;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayerMask);
        //����
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<HealthManager>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
