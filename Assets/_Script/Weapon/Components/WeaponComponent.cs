using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuyBui.Weapons.Components
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected Weapon weapon;

        protected bool isAttackActive;

        protected virtual void Awake()
        {
            weapon = GetComponent<Weapon>();
        }

        protected virtual void HandleEnter()
        {
            isAttackActive = true;
        }

        protected virtual void HandleExit()
        {
            isAttackActive = false;
        }

        protected virtual void OnEnable()
        {
            weapon.onEnter += HandleEnter;
            weapon.onExit += HandleExit;

        }
        protected virtual void OnDisable()
        {
            weapon.onEnter -= HandleEnter;
            weapon.onExit -= HandleExit;
        }

    }
}
