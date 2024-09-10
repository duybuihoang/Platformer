using DuyBui.Weapons.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace DuyBui.Weapons
{
    [CreateAssetMenu(fileName ="newWeaponData", menuName ="Data/Weapon Data/Basic Weapon Data", order = 0)]
    public class WeaponDataSO : ScriptableObject
    {
        [field :SerializeField] public int NumberOfAttacks { get; private set; }

        [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }

        public T getData<T>()
        {
            return ComponentData.OfType<T>().FirstOrDefault();
        }

        public List<Type> GetAllDependency()
        {
            return ComponentData.Select(component => component.ComponentDependency).ToList();
        }    
        
        public void AddData(ComponentData data)
        {
            if (ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null)
            {
                Debug.LogWarning($"Component {data.GetType().ToString()} Already Exist!!");
                return;
            }
            ComponentData.Add(data);
        }


       
    }
}
