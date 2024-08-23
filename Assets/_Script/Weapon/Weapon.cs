using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuyBui.Weapons
{
    public class Weapon : MonoBehaviour
    {
        private Animator anim;
        private GameObject baseGameObject;


        public void Enter()
        {
            print($"{transform.name} enter");

            anim.SetBool("active", true);
        }

        private void Awake()
        {
            baseGameObject = transform.Find("Base").gameObject;
            anim = baseGameObject.GetComponent<Animator>();
        }

    }
}