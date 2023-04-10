using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // ссылка на камеру
    Camera mainCamera;
    // ссылка на игрока
    GameObject player;

    // смещение позиции камеры относительно позиции игрока
    [SerializeField] Vector2 positionOffset = new Vector2(0f, 1f);

    // используется ли плавное движение камеры
    [SerializeField] bool useSmoothMovement = true;
    // скорость плавного движения
    [SerializeField] float smoothSpeed = 0.1f;
    // текущая скорость (необходимо для функции SmoothDamp)
    Vector3 velocity = Vector3.zero;

    // используются ли эффекты тряски
    [SerializeField] bool useShakeEffects = true;
    // интенсивность тряски
    [SerializeField] float shakeDuration = 0.5f;
    // кривая изменения интенсивности тряски
    [SerializeField] AnimationCurve shakeIntensityCurve;

    void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();
        player = FindObjectOfType<Person>().gameObject;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3
        (
            player.transform.position.x + positionOffset.x,
            player.transform.position.y + positionOffset.y,
            transform.position.z
        );

        if (useSmoothMovement)
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
        else
            transform.position = targetPosition;
    }

    // произвести тряску камеры
    public void ShakeOnce(float intensity)
    {
        if (useShakeEffects)
            StartCoroutine(ShakeOnceCoroutine(intensity));
    }

    // корутина для эффекта тряски камеры
    IEnumerator ShakeOnceCoroutine(float intensity)
    {
        float t = 0;
        
        while (t < shakeDuration)
        {
            t += Time.deltaTime;
            float strength = intensity * shakeIntensityCurve.Evaluate(t / shakeDuration);
            mainCamera.transform.localPosition = Vector2.zero + Random.insideUnitCircle * strength;
            yield return new WaitForEndOfFrame();
        }

        mainCamera.transform.localPosition = Vector3.zero;
    }
}
