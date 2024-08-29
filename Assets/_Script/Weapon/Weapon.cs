using System;
using UnityEngine;
using DuyBui.Utilities;
using DuyBui.CoreSystem;

namespace DuyBui.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [field: SerializeField]public WeaponDataSO Data { get; private set; }
        [SerializeField] private float attackCounterResetCooldown;

        public int CurrentAttackCounter
        { 
            get => currentAttackCounter;
            private set
            {
                //currentAttackCounter = value % numberOfAttacks;

                currentAttackCounter = value >= Data.NumberOfAttacks ? 0 : value;
            } 
        }


        public event Action onEnter;
        public event Action onExit;


        private Animator anim;
        public GameObject BaseGameObject { get; private set; }
        public GameObject WeaponSpriteGameObject { get; private set; }

        public AnimationEventHandler EventHandler { get; private set; }
        public Core Core { get; private set; }

        private int currentAttackCounter;

        private Timer attackCounterResetTimer;


        public void Enter()
        {
            print($"{transform.name} enter");

            attackCounterResetTimer.StopTimer();

            anim.SetBool("active", true);
            anim.SetInteger("counter", CurrentAttackCounter);

            onEnter?.Invoke();
        }

        public void SetCore(Core core)
        {
            Core = core;
        }

        private void Exit()
        {
            anim.SetBool("active", false);
            CurrentAttackCounter++;
            attackCounterResetTimer.StartTimer();
            onExit?.Invoke();
        }

        private void Awake()
        {
            BaseGameObject = transform.Find("Base").gameObject;
            WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;
            anim = BaseGameObject.GetComponent<Animator>();

            EventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();

            attackCounterResetTimer = new Timer(attackCounterResetCooldown);
        }

        private void Update()
        {
            attackCounterResetTimer.Tick();
        }

        private void ResetAttackCounter() => CurrentAttackCounter = 0;  

        private void OnEnable()
        {
            EventHandler.OnFinish += Exit;
            attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
        }

        private void OnDisable()
        {
            EventHandler.OnFinish -= Exit;
            attackCounterResetTimer.OnTimerDone -= ResetAttackCounter;

        }
    }
}
