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

        public event Action onExit;

        private Animator anim;
        private GameObject baseGameObject;

        private AnimationEventHandler eventHandler;

        private int currentAttackCounter;

        private Timer attackCounterResetTimer;


        public void Enter()
        {
            print($"{transform.name} enter");

            attackCounterResetTimer.StopTimer();

            anim.SetBool("active", true);
            anim.SetInteger("counter", CurrentAttackCounter);
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
            baseGameObject = transform.Find("Base").gameObject;
            anim = baseGameObject.GetComponent<Animator>();

            eventHandler = baseGameObject.GetComponent<AnimationEventHandler>();

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
