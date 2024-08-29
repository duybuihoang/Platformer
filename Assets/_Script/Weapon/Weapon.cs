using System;
using UnityEngine;
using DuyBui.Utilities;

namespace DuyBui.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int numberOfAttacks;
        [SerializeField] private float attackCounterResetCooldown;

        public int CurrentAttackCounter
        { 
            get => currentAttackCounter;
            private set
            {
                //currentAttackCounter = value % numberOfAttacks;

                currentAttackCounter = value >= numberOfAttacks ? 0 : value;
            } 
        }


        public event Action onEnter;
        public event Action onExit;


        private Animator anim;
        public GameObject BaseGameObject { get; private set; }
        public GameObject WeaponSpriteGameObject { get; private set; }

        private AnimationEventHandler eventHandler;

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

            eventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();

            attackCounterResetTimer = new Timer(attackCounterResetCooldown);
        }

        private void Update()
        {
            attackCounterResetTimer.Tick();
        }

        private void ResetAttackCounter() => CurrentAttackCounter = 0;  

        private void OnEnable()
        {
            eventHandler.onFinish += Exit;
            attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
        }

        private void OnDisable()
        {
            eventHandler.onFinish -= Exit;
            attackCounterResetTimer.OnTimerDone -= ResetAttackCounter;

        }
    }
}
