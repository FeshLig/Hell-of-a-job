using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    // ссылка на Rigidbody
    Rigidbody2D rb;

    // сила отбрасывания
    [SerializeField] float force = 8f;
    // множитель силы отбрасывания
    [SerializeField] float forceMultiplier = 1f;
    // длительность блокировки движения при отбрасывании в секундах
    [SerializeField] [Range(0.1f, 1.0f)] float paralysisDuration = 0.25f;
    // угол отбрасывания
    [SerializeField] float angle = 20f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // применить отбрасывание
    public void ApplyKnockback(Vector2 direction)
    {
        StartCoroutine(ApplyKnockbackCoroutine(direction));
    }

    // применить отбрасывание с дополнительным множителем
    public void ApplyKnockback(Vector2 direction, float multiplier)
    {
        StartCoroutine(ApplyKnockbackCoroutine(direction, multiplier));
    }

    // корутина для применения отбрасывания с блокировкой движения
    IEnumerator ApplyKnockbackCoroutine(Vector2 direction, float multiplier = 1f)
    {
        direction.y = 0;
        direction.Normalize();
        direction.y = Mathf.Sin(Mathf.Deg2Rad * angle); // противолежащий катет к углу angle в треугольнике с гипотенузой 1

        GetComponent<IMoving>().DisableMovement();

        rb.velocity = Vector2.zero;
        rb.AddForce(direction.normalized * force * forceMultiplier * multiplier, ForceMode2D.Impulse);
        yield return new WaitForSeconds(paralysisDuration);

        GetComponent<IMoving>().EnableMovement();
    }
}
