using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuyBui.CoreSystem
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        [SerializeField] private GameObject damageParticles;

        private CoreComp<Stats> stats;
        private CoreComp<ParticleManager> particleManager;



        public void Damage(float amount)
        {
            Debug.Log(core.transform.parent.name + " Damaged!");
            stats.Comp?.DecreaseHealth(amount);
            particleManager.Comp?.StartParticleWithRandomRotation(damageParticles);
        }

        protected override void Awake()
        {
            base.Awake();

            stats = new CoreComp<Stats>(core);
            particleManager = new CoreComp<ParticleManager>(core);
        }
    }
}