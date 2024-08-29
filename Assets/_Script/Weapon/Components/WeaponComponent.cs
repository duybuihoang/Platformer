using DuyBui.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuyBui.Weapons.Components
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected Weapon weapon;
        //TODO: FIX THIS WHEN FINISHING WEAPON DATA
        //protected AnimationEventHandler EvenHandler => weapon.EventHandler;
        protected AnimationEventHandler evenHandler ;
        protected Core Core => weapon.Core;

        protected bool isAttackActive;

        protected virtual void Awake()
        {
            weapon = GetComponent<Weapon>();
            evenHandler = GetComponentInChildren<AnimationEventHandler>();
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
