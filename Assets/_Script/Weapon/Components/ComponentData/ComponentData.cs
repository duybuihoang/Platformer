using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuyBui.Weapons.Components
{
    [Serializable]
    public class ComponentData 
    {
        
    }

    [Serializable]
    public class ComponentData<T>:ComponentData where T: AttackData
    {
        [field: SerializeField] public T[] AttackData { get; private set; }
    }
} 
