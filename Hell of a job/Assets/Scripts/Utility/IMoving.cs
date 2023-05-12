using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoving
{
    // включить движение
    public void EnableMovement();
    // выключить движение
    public void DisableMovement();
    // полностью выключить скрипт
    public void DisableCompletely();
}
