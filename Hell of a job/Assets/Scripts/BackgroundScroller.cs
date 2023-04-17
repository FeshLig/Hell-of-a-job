using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    // ссылка на transform камеры
    Transform mainCamera;
    // скорость движения фона относительно скорости движения камеры
    [SerializeField, Range(0f, 1f)] float scrollSpeed = 0.9f;
    // включено ли движение фона по вертикали
    [SerializeField] bool enableVerticalScrolling = true;

    // позиция камеры в предыдущем кадре
    Vector3 mainCameraPreviousPosition;

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>().transform;
        mainCameraPreviousPosition = mainCamera.position;
    }

    void Update()
    {
        Vector3 delta = mainCamera.position - mainCameraPreviousPosition;
        
        if (!enableVerticalScrolling)
            delta.y = 0f;

        transform.position -= delta * scrollSpeed;

        mainCameraPreviousPosition = mainCamera.position;
    }
}
