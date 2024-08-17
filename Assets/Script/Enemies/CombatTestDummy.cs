using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CombatTestDummy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject hitParticle;
    
    private Animator anim;

    public void Damage(float amount)
    {
        Debug.Log(amount + " Damage taken");

        Instantiate(hitParticle, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0.0f, 360.0f)));
        anim.SetTrigger("damage");
        Destroy(gameObject);
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }





}
