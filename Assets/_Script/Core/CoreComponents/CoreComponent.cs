using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuyBui.CoreSystem
{
    public class CoreComponent : MonoBehaviour, ILogicUpdate
    {
        protected Core core;

        public virtual void LogicUpdate()
        {

        }

        protected virtual void Awake()
        {
            core = transform.parent.GetComponent<Core>();

            if (core == null)
            {
                Debug.LogError("There is no Core on the parent");
            }
            core.AddComponent(this);

        }



    }
}