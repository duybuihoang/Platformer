using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeaponAttackDetails
{
    public string attackName;
    public float moveSpeed;
    public float damageAmount;

    public float knockbackStrength;
    public Vector2 knockbackAngle;
}
