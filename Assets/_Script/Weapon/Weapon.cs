using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuyBui.Weapons
{
    public class Weapon : MonoBehaviour
    {
        public event Action onExit;

        private Animator anim;
        private GameObject baseGameObject;

        private AnimationEventHandler eventHandler;


        public void Enter()
        {
            print($"{transform.name} enter");

            anim.SetBool("active", true);
        }

        private void Exit()
        {
            anim.SetBool("active", false);

            onExit?.Invoke();
        }

        private void Awake()
        {
            baseGameObject = transform.Find("Base").gameObject;
            anim = baseGameObject.GetComponent<Animator>();

            eventHandler = baseGameObject.GetComponent<AnimationEventHandler>();
        }

        private void OnEnable()
        {
            eventHandler.onFinish += Exit;
        }

        private void OnDisable()
        {
            eventHandler.onFinish -= Exit;
        }
    }
}
