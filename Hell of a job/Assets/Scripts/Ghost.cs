using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy, IMoving
{
    // ссылка на спрайт
    SpriteRenderer spriteRenderer;

    // включено ли движение
    bool enableMovement = true;

    [Header("Ghost Properties")]
    
    // скорость полёта
    [SerializeField] float flightSpeed = 2f;
    // скорость рывка к игроку
    [SerializeField] float chargeSpeed = 10f;

    // урон при рывке
    [SerializeField] float chargeDamage = 25f;
    // урон от столкновения
    float defaultCollisionDamage;

    // находится ли дух в рывке
    bool isCharging = false;
    // находится ли дух в невидимости
    bool isInvisible = false;
    // остановился ли дух в точке столкновения с игроком
    bool isStayingAtCollisionPosition = false;

    // время, на которое дух останавливается после столкновения с игроком, в секундах
    [SerializeField] float stayAtCollisionPositionForSeconds = 0.125f;

    // минимальный интервал между рывками в секундах
    [SerializeField] float minChargeTimeInterval = 5f;
    // максимальный интервал между рывками в секундах
    [SerializeField] float maxChargeTimeInterval = 10f;
    // интервал до следующего рывка в секундах (выбирается рандомно)
    float currentChargeTimeInterval;
    // позиция, куда осуществляется рывок
    Vector3 chargeTargetPosition;
    // необходимое расстояние до игрока при пролёте насквозь, после которого рывок завершается
    [SerializeField] float chargeOvershoot = 3f;
    // время, на которое дух останавливается после рывка, в секундах
    [SerializeField] float stayAtChargePositionForSeconds = 0.25f;

    // интервал между периодами невидимости в секундах
    [SerializeField] float invisibiltyTimeInterval = 5f;
    // время нахождения духа в невидимости в секундах
    [SerializeField] float stayInvisibleForSeconds = 1f;
    // скорость изменения прозрачности спрайта
    [SerializeField] float spriteAlphaChangeSpeed = 2f;

    // переменная для отсчёта времени до рывка
    float chargeT = 0;
    // переменная для отсчёта времени до перехода в невидимость
    float invisibiltyT = 0;
    // переменная для отсчёта времени до выхода из невидимости
    float stayInvisibleT = 0;

    new void Start()
    {
        base.Start();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        defaultCollisionDamage = base.collisionDamage;

        currentChargeTimeInterval = Random.Range(minChargeTimeInterval, maxChargeTimeInterval);
    }

    new void Update()
    {
        base.Update();

        HandleInvisibility();

        if (isAggressive && enableMovement)
            HandleMovement();
    }

    // вход и выход из невидимости
    void HandleInvisibility()
    {
        if (isInvisible)
        {
            stayInvisibleT += Time.deltaTime;
            
            if (stayInvisibleT >= stayInvisibleForSeconds)
            {
                stayInvisibleT = 0;
                isInvisible = false;
                StartCoroutine(ChangeSpriteAlpha(1f));
            }
        }
        else if (isAggressive)
        {
            invisibiltyT += Time.deltaTime;

            if (invisibiltyT >= invisibiltyTimeInterval)
            {
                invisibiltyT = 0;
                StartCoroutine(ChangeSpriteAlpha(0f));
            }
        }
    }

    // передвижение
    void HandleMovement()
    {
        spriteRenderer.flipX = transform.position.x < player.transform.position.x;

        if (isCharging)
        {
            transform.position = Vector2.MoveTowards(transform.position, chargeTargetPosition, chargeSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, chargeTargetPosition) <= 0.1f)
                StartCoroutine(StayAtChargeTargetPosition());
        }
        else
        {
            if (!isStayingAtCollisionPosition)
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, flightSpeed * Time.deltaTime);

            chargeT += Time.deltaTime;

            if (chargeT >= currentChargeTimeInterval)
            {
                chargeT = 0;
                StartCharging();
            }
        }
    }

    // начать рывок
    void StartCharging()
    {
        isCharging = true;
        base.collisionDamage = chargeDamage;
        chargeTargetPosition = player.transform.position + (Vector3)((Vector2)player.transform.position - (Vector2)transform.position).normalized * chargeOvershoot;
    }

    // корутина для временной остановки после рывка
    IEnumerator StayAtChargeTargetPosition()
    {
        yield return new WaitForSeconds(stayAtChargePositionForSeconds);
        StopCharging();
    }

    // завершить рывок
    void StopCharging()
    {
        isCharging = false;
        base.collisionDamage = defaultCollisionDamage;
        currentChargeTimeInterval = Random.Range(minChargeTimeInterval, maxChargeTimeInterval);
    }

    new public void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);

        if (col.CompareTag("Player") && !isCharging)
            StartCoroutine(StayAtCollisionPosition());
    }

    // корутина для временной остановки после столкновения с игроком
    IEnumerator StayAtCollisionPosition()
    {
        isStayingAtCollisionPosition = true;
        yield return new WaitForSeconds(stayAtCollisionPositionForSeconds);
        isStayingAtCollisionPosition = false;
    }

    // плавное изменение прозрачности спрайта при переходе в невидимость
    IEnumerator ChangeSpriteAlpha(float targetAlpha)
    {
        while (spriteRenderer.color.a != targetAlpha)
        {
            spriteRenderer.color = new Color
            (
                spriteRenderer.color.r,
                spriteRenderer.color.g,
                spriteRenderer.color.b,
                Mathf.MoveTowards(spriteRenderer.color.a, targetAlpha, spriteAlphaChangeSpeed * Time.deltaTime)
            );

            yield return new WaitForEndOfFrame();
        }

        if (targetAlpha == 0f)
            isInvisible = true;
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
        StopCharging();
    }
}
