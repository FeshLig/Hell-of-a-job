using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // ссылка на компонент новой системы ввода
    PlayerInput playerInput;

    // ссылки на действия ввода
    public InputAction move, jump, dash, lightAttack, heavyAttack, specialAttack, interact, chooseWeapon, map, sins, pause;

    void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();

        move          = playerInput.actions["Move"];
        jump          = playerInput.actions["Jump"];
        dash          = playerInput.actions["Dash"];
        lightAttack   = playerInput.actions["Light Attack"];
        heavyAttack   = playerInput.actions["Heavy Attack"];
        specialAttack = playerInput.actions["Special Attack"];
        interact      = playerInput.actions["Interact"];
        chooseWeapon  = playerInput.actions["Choose Weapon"];
        map           = playerInput.actions["Map"];
        sins          = playerInput.actions["Sins"];
        pause         = playerInput.actions["Pause"];
    }
}
