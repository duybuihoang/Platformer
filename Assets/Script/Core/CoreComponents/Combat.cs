using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    [SerializeField] private GameObject damageParticles;

    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    protected CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    private ParticleManager ParticleManager => particleManager ? particleManager : core.GetCoreComponent(ref particleManager);

    private Movement movement;
    private CollisionSenses collisionSenses;
    private Stats stats;
    private ParticleManager particleManager; 

    [SerializeField] private float maxKnockbackTime = 0.2f;

    private bool isKnockbackActive;
    private float knockbackStartTimer;

    public override void LogicUpdate()
    {
        CheckKnockback();
    }
    public void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + " Damaged!");
        Stats?.DecreaseHealth(amount);
        ParticleManager.StartParticleWithRandomRotation(damageParticles);
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        Movement?.SetVelocity(strength, angle, direction);
        Movement.canSetVelocity = false ;
        isKnockbackActive = true;
        knockbackStartTimer = Time.time;
    }

    private void CheckKnockback()
    {
        if (isKnockbackActive && ((Movement?.CurrentVelocity.y <= 0.01f && CollisionSenses.Ground)
             || Time.time >= knockbackStartTimer + maxKnockbackTime ))
        {
            isKnockbackActive = false;
            Movement.canSetVelocity = true;
        }
    }
}
