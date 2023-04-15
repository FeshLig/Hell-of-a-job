using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : HealthManager
{
    // ссылка на контроллер камеры
    CameraController cameraController;

    void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
    }

    // получить урон
    new public void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        cameraController.ShakeOnce(amount * damageMultiplier / maxHealth);
    }

    /*
    // пасхалка для любителей читать чужой код :)
    // остановка времени при получении урона (как в Hollow Knight)
    IEnumerator TakeDamageCoroutine(float amount)
    {
        Time.timeScale = 0f;
        base.TakeDamage(amount);
        yield return new WaitForSecondsRealtime(0.25f);
        Time.timeScale = 1f;
        cameraController.ShakeOnce(amount * damageMultiplier / maxHealth);
    }
    */
}
