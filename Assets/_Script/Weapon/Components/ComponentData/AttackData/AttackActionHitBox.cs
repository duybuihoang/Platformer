using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuyBui.Weapons.Components
{
    [Serializable]
    public class AttackActionHitBox :AttackData
    {
        public bool Debug;
        [field: SerializeField] public Rect HitBox { get; private set; }

    }
}
