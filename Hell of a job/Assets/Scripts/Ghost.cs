using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    // ссылка на Transform игрока
    Transform playerTransform;

    // ссылка на спрайт
    SpriteRenderer spriteRenderer;

    // расстояние, на котором призрак замечает игрока
    [SerializeField] float playerDetectionDistance = 10f;

    // включено ли агрессивное поведение по отношению к игроку
    bool enableBehaviour = false;
    // включено ли движение
    bool enableMovement = true;

    // скорость полёта
    [SerializeField] float flightSpeed = 2f;
    // скорость рывка к игроку
    [SerializeField] float chargeSpeed = 10f;

    // находится ли призрак в рывке
    bool isCharging = false;
    // находится ли призрак в невидимости
    bool isInvisible = false;

    // минимальный интервал между рывками в секундах
    [SerializeField] float minChargeTimeInterval = 5f;
    // максимальный интервал между рывками в секундах
    [SerializeField] float maxChargeTimeInterval = 10f;
    // интервал до следующего рывка в секундах (выбирается рандомно)
    [SerializeField] float currentChargeTimeInterval;

    // позиция, куда осуществляется рывок
    Vector3 chargeTargetPos;
    // необходимое расстояние до игрока при пролёте насквозь, после которого рывок завершается
    [SerializeField] float chargeOvershoot = 3f;

    // интервал между периодами невидимости в секундах
    [SerializeField] float invisibiltyTimeInterval = 5f;
    // время нахождения призрака в невидимости в секундах
    [SerializeField] float stayInvisibleForSeconds = 1f;
    // скорость изменения прозрачности спрайта
    [SerializeField] float spriteAlphaChangeSpeed = 2f;

    // переменная для отсчёта времени до рывка
    float chargeT = 0;
    // переменная для отсчёта времени до перехода в невидимость
    float invisibiltyT = 0;
    // переменная для отсчёта времени до выхода из невидимости
    float stayInvisibleT = 0;

    void Start()
    {
        playerTransform = FindObjectOfType<Person>().transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        currentChargeTimeInterval = Random.Range(minChargeTimeInterval, maxChargeTimeInterval);
    }

    void Update()
    {
        enableBehaviour = Vector2.Distance((Vector2)transform.position, (Vector2)playerTransform.position) <= playerDetectionDistance;

        HandleInvisibility();

        if (enableBehaviour && enableMovement)
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
                StopAllCoroutines();
                StartCoroutine(ChangeSpriteAlpha(1f));
            }
        }
        else if (enableBehaviour)
        {
            invisibiltyT += Time.deltaTime;

            if (invisibiltyT >= invisibiltyTimeInterval)
            {
                invisibiltyT = 0;
                StopAllCoroutines();
                StartCoroutine(ChangeSpriteAlpha(0f));
            }
        }
    }

    // передвижение
    void HandleMovement()
    {
        if (isCharging)
        {
            transform.position = Vector2.MoveTowards(transform.position, chargeTargetPos, chargeSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, chargeTargetPos) <= 0.1f)
            {
                isCharging = false;
                currentChargeTimeInterval = Random.Range(minChargeTimeInterval, maxChargeTimeInterval);
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, flightSpeed * Time.deltaTime);  

            chargeT += Time.deltaTime;

            if (chargeT >= currentChargeTimeInterval)
            {
                chargeT = 0;
                chargeTargetPos = playerTransform.position + (Vector3)((Vector2)playerTransform.position - (Vector2)transform.position).normalized * chargeOvershoot;
                isCharging = true;
            }
        }
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
}
