using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField]
    private bool combatEnabled;
    [SerializeField]
    private float inputTimer, attack1Radius, attack1Damage;


    [SerializeField]
    private Transform attack1HitBox;
    [SerializeField]
    private LayerMask whatIsDamagable;

    private bool gotInput, isAttacking, isFirstAttack;  

    private float lastInputTime = Mathf.NegativeInfinity;

    private float[] attackDetail = new float[2];

    private Animator anim;

    private PlayerController PC;

    private PlayerStat PS;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
        PC = GetComponent<PlayerController>();
        PS = GetComponent<PlayerStat>();  
    }
    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }

    private void CheckCombatInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(combatEnabled)
            {
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    private void CheckAttacks()
    {
        if(gotInput)       
        {
            //perform attack1
            if(!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                anim.SetBool("attack1", true);
                anim.SetBool("firstAttack", isFirstAttack);
                anim.SetBool("isAttacking", isAttacking);
            }
        }

        if(Time.time >= lastInputTime + inputTimer)
        {
            gotInput = false;
        }

    }

    private void CheckAttackHitBox()
    {

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBox.position, attack1Radius, whatIsDamagable);

        attackDetail[0] = attack1Damage;
        attackDetail[1] = transform.position.x;

        foreach(Collider2D collider in detectedObjects)
        {

            collider.transform.parent.SendMessage("Damage", attackDetail);
            //Instantiate hit Particle
        }
    }

    private void FinishAttack1()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("attack1", false);
    }

    private void Damage(float[] attackDetails)
    {
        if (!PC.getDashStatus())
        {
            int direction;

            //Damage player here using attackDetails[0]
            PS.DecreaseHealth(attackDetail[0]);

            if (attackDetails[1] < transform.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            PC.Knockback(direction);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBox.position, attack1Radius);
    }

}
