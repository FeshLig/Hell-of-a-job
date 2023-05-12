using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // список оружия
    [SerializeField] List<Weapon> weapons = new List<Weapon>();
    // индекс оружия в руках персонажа
    [SerializeField] int currentWeaponIndex = -1;
    // оружие в руках персонажа
    public Weapon CurrentWeapon { get { return weapons[currentWeaponIndex]; } }
    // пуст ли инвентарь
    public bool IsEmpty { get { return weapons.Count == 0; } }
    
    // подобрать оружие
    public void PickUp(Weapon weapon)
    {
        weapons.Add(weapon);

        if (weapons.Count == 1)
            SwitchCurrentWeaponTo(0);
    }

    // переключить оружие
    public void SwitchCurrentWeaponTo(int index)
    {
        currentWeaponIndex = index;
        // TO DO: переключение спрайта оружия в руках персонажа, возможно смена режима аниматора
    }
}
