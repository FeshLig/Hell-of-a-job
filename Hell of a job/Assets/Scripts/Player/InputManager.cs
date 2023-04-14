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

    // статический экземпляр класса для использования другими классами
    static InputManager instance;

    // получить статический экземпляр
    public static InputManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("There's more than one Input Manager in the scene");
        
        instance = this;
    }

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
    
    // методы для получения различных состояний каждого действия ввода
    // (нажато в этом кадре, нажато вообще, отжато в этом кадре, числовое значение)

    public static bool MoveWasPressedThisFrame { get { return instance.move.WasPressedThisFrame(); } }
    public static bool MoveIsPressed { get { return instance.move.IsPressed(); } }
    public static bool MoveWasReleasedThisFrame { get { return instance.move.WasReleasedThisFrame(); } }
    public static float MoveValue { get { return instance.move.ReadValue<float>(); } }

    public static bool JumpWasPressedThisFrame { get { return instance.jump.WasPressedThisFrame(); } }
    public static bool JumpIsPressed { get { return instance.jump.IsPressed(); } }
    public static bool JumpWasReleasedThisFrame { get { return instance.jump.WasReleasedThisFrame(); } }
    public static float JumpValue { get { return instance.jump.ReadValue<float>(); } }

    public static bool DashWasPressedThisFrame { get { return instance.dash.WasPressedThisFrame(); } }
    public static bool DashIsPressed { get { return instance.dash.IsPressed(); } }
    public static bool DashWasReleasedThisFrame { get { return instance.dash.WasReleasedThisFrame(); } }
    public static float DashValue { get { return instance.dash.ReadValue<float>(); } }

    public static bool LightAttackWasPressedThisFrame { get { return instance.lightAttack.WasPressedThisFrame(); } }
    public static bool LightAttackIsPressed { get { return instance.lightAttack.IsPressed(); } }
    public static bool LightAttackWasReleasedThisFrame { get { return instance.lightAttack.WasReleasedThisFrame(); } }
    public static float LightAttackValue { get { return instance.lightAttack.ReadValue<float>(); } }

    public static bool HeavyAttackWasPressedThisFrame { get { return instance.heavyAttack.WasPressedThisFrame(); } }
    public static bool HeavyAttackIsPressed { get { return instance.heavyAttack.IsPressed(); } }
    public static bool HeavyAttackWasReleasedThisFrame { get { return instance.heavyAttack.WasReleasedThisFrame(); } }
    public static float HeavyAttackValue { get { return instance.heavyAttack.ReadValue<float>(); } }

    public static bool SpecialAttackWasPressedThisFrame { get { return instance.specialAttack.WasPressedThisFrame(); } }
    public static bool SpecialAttackIsPressed { get { return instance.specialAttack.IsPressed(); } }
    public static bool SpecialAttackWasReleasedThisFrame { get { return instance.specialAttack.WasReleasedThisFrame(); } }
    public static float SpecialAttackValue { get { return instance.specialAttack.ReadValue<float>(); } }

    public static bool InteractWasPressedThisFrame { get { return instance.interact.WasPressedThisFrame(); } }
    public static bool InteractIsPressed { get { return instance.interact.IsPressed(); } }
    public static bool InteractWasReleasedThisFrame { get { return instance.interact.WasReleasedThisFrame(); } }
    public static float InteractValue { get { return instance.interact.ReadValue<float>(); } }

    public static bool ChooseWeaponWasPressedThisFrame { get { return instance.chooseWeapon.WasPressedThisFrame(); } }
    public static bool ChooseWeaponIsPressed { get { return instance.chooseWeapon.IsPressed(); } }
    public static bool ChooseWeaponWasReleasedThisFrame { get { return instance.chooseWeapon.WasReleasedThisFrame(); } }
    public static float ChooseWeaponValue { get { return instance.chooseWeapon.ReadValue<float>(); } }

    public static bool MapWasPressedThisFrame { get { return instance.map.WasPressedThisFrame(); } }
    public static bool MapIsPressed { get { return instance.map.IsPressed(); } }
    public static bool MapWasReleasedThisFrame { get { return instance.map.WasReleasedThisFrame(); } }
    public static float MapValue { get { return instance.map.ReadValue<float>(); } }

    public static bool SinsWasPressedThisFrame { get { return instance.sins.WasPressedThisFrame(); } }
    public static bool SinsIsPressed { get { return instance.sins.IsPressed(); } }
    public static bool SinsWasReleasedThisFrame { get { return instance.sins.WasReleasedThisFrame(); } }
    public static float SinsValue { get { return instance.sins.ReadValue<float>(); } }

    public static bool PauseWasPressedThisFrame { get { return instance.pause.WasPressedThisFrame(); } }
    public static bool PauseIsPressed { get { return instance.pause.IsPressed(); } }
    public static bool PauseWasReleasedThisFrame { get { return instance.pause.WasReleasedThisFrame(); } }
    public static float PauseValue { get { return instance.pause.ReadValue<float>(); } }
}
