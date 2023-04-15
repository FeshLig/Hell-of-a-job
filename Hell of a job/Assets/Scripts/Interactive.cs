using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    // компонент с основным кодом объекта
    [SerializeField] Component component;
    // метод в компоненте component, который должен выполниться при взаимодействии с объектом
    [SerializeField] string methodName;

    // активация метода methodName в компоненте component
    public void Activate()
    {
        component.SendMessage(methodName);
    }
}
