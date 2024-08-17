using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttackDetails 
{
    public Vector2 position;

    public float damageAmount;

    public float stunDamageAmount;

}

[System.Serializable]
public struct WeaponAttackDetails
{
    public string attackName;
    public float moveSpeed;
    public float damageAmount;
}
