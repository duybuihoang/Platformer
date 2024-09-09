using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace DuyBui.CoreSystem
{
    public class KnockBackReceiver : CoreComponent, IKnockBackable
    {



        //[FormerlySerializedAs("maxKnockbackTime")][SerializeField] private float maxKnockBackTime = 0.2f;
        [SerializeField] private float maxKnockBackTime = 0.2f;

        private CoreComp<Movement> movement;
        private CoreComp<CollisionSenses> collisionSenses;

        private bool isKnockBackActive;
        private float knockBackStartTimer;

        public override void LogicUpdate()
        {
            CheckKnockBack();
        }
        

        public void KnockBack(Vector2 angle, float strength, int direction)
        {
            movement.Comp?.SetVelocity(strength, angle, direction);
            movement.Comp.canSetVelocity = false;
            isKnockBackActive = true;
            knockBackStartTimer = Time.time;
        }

        private void CheckKnockBack()
        {
            if (isKnockBackActive && (movement.Comp?.CurrentVelocity.y <= 0.01f && collisionSenses.Comp.Ground
                 || Time.time >= knockBackStartTimer + maxKnockBackTime))
            {
                isKnockBackActive = false;
                movement.Comp.canSetVelocity = true;
            }
        }
        protected override void Awake()
        {
            base.Awake();

            movement = new CoreComp<Movement>(core);
            collisionSenses = new CoreComp<CollisionSenses>(core);
        }
    }
}