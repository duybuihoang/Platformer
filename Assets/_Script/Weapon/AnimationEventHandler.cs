using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuyBui.Weapons
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action onFinish;

        private void AnimationFinishTrigger() => onFinish?.Invoke();
    }
}
