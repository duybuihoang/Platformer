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

        protected virtual void Start()
        {

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


    public abstract class WeaponComponent<T1, T2> : WeaponComponent where T1: ComponentData<T2> where T2: AttackData
    {
        protected T1 data;
        protected T2 currentAttackData;

        protected override void HandleEnter()
        {
            base.HandleEnter();

            currentAttackData = data.AttackData[weapon.CurrentAttackCounter];

        }

        protected override void Awake()
        {
            base.Awake();

            data = weapon.Data.getData<T1>();
        }
    }


}
