using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuyBui.Weapons.Components
{
    public class WeaponSprite : WeaponComponent
    {
        private SpriteRenderer baseSpriteRenderer;
        private SpriteRenderer weaponSpriteRenderer;

        [SerializeField] private WeaponSprites[] weaponSprites;

        private int currentWeaponSpriteIndex;

        protected  override void HandleEnter() 
        {
            base.HandleEnter();
             
            currentWeaponSpriteIndex = 0;
        }  
       

        private void HandleBaseSpriteChange(SpriteRenderer sr)
        {
            if(!isAttackActive)
            {
                weaponSpriteRenderer.sprite = null;
                return;
            }

            var currentAttackSprites = weaponSprites[weapon.CurrentAttackCounter].Sprites;
            if(currentWeaponSpriteIndex >= currentAttackSprites.Length)
            {
                Debug.LogWarning($"{weapon.name} weapon sprite length mismatch");
                return;
            }

            weaponSpriteRenderer.sprite = currentAttackSprites[currentWeaponSpriteIndex];

            currentWeaponSpriteIndex++;

        }

        protected override void Awake()
        {
            base.Awake();

            baseSpriteRenderer = transform.Find("Base").GetComponent<SpriteRenderer>();
            weaponSpriteRenderer = transform.Find("WeaponSprite").GetComponent<SpriteRenderer>();


            //TODO: FIX THIS WHEN CREATE WEAPON DATA
            /*baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
            weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();*/
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            baseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);
            weapon.onEnter += HandleEnter;
        }
        protected override void OnDisable()
        {
            base.OnDisable();

            baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
            weapon.onEnter -= HandleEnter;
        }

    }

    [Serializable]
    public class WeaponSprites
    {
        [field: SerializeField] public Sprite[] Sprites { get; private set; }

    }
}
