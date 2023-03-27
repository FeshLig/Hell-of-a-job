using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    // компонент с основным кодом объекта
    [SerializeField] Component component;
    // метод внутри компонента component, который должен выполниться при взаимодействии с объектом
    [SerializeField] string methodName;

    // метод, вызываемый при взаимодействии с объектом
    public void Activate()
    {
        component.SendMessage(methodName);
    }
}
