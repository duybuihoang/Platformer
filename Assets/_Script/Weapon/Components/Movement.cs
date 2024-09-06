using DuyBui.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuyBui.Weapons.Components
{
    public class Movement : WeaponComponent<MovementData, AttackMovement>
    {
        private CoreSystem.Movement coreMovement;
        private CoreSystem.Movement CoreMovement => coreMovement ? coreMovement : Core.GetCoreComponent(ref coreMovement);



        private void HandleStartMovement()
        {
            CoreMovement.SetVelocity(currentAttackData.Velocity, currentAttackData.Direction, coreMovement.FacingDirection);
        }
        private void HandleStopMovement()
        {
            CoreMovement.SetVelocityZero();
        }


        protected override void OnEnable()
        {
            base.OnEnable();

            evenHandler.OnStartMovement += HandleStartMovement;
            evenHandler.OnStopMovement += HandleStopMovement;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            evenHandler.OnStartMovement -= HandleStartMovement;
            evenHandler.OnStopMovement -= HandleStopMovement;

        }
    }
}
