using System;
using System.Collections.Generic;
using TouchDevUltimate;
using TouchDevUltimate.Gameplay.Character;
using UnityEngine;

public class CharacterWeaponUser : CharacterAbility
{
    public Weapon equippedWeapon;
    public GameObject weaponModel;

    [Header("Inputs")]
    public string weaponInput;

    protected override void Init()
    {
        equippedWeapon.SetUser(GetComponent<Character>());
    }

    public override void OnGesture(string name, GestureType type)
    {
        if (name == weaponInput && type == GestureType.Tap)
        {
            equippedWeapon.ShootRequest();
        }
    }
}
