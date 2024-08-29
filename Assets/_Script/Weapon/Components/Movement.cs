using DuyBui.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuyBui.Weapons.Components
{
    public class Movement : WeaponComponent
    {
        private CoreSystem.Movement coreMovement;
        private CoreSystem.Movement CoreMovement => coreMovement ? coreMovement : Core.GetCoreComponent(ref coreMovement);

        private MovementData data;


        private void HandleStartMovement()
        {
            var currentAttackData = data.AttackData[weapon.CurrentAttackCounter];

            CoreMovement.SetVelocity(currentAttackData.Velocity, currentAttackData.Direction, coreMovement.FacingDirection);
        }
        private void HandleStopMovement()
        {
            CoreMovement.SetVelocityZero();
        }

        protected override void Awake()
        {
            base.Awake();
            data = weapon.Data.getData<MovementData>();
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
