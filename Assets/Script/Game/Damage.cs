using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Damage : MonoBehaviour
    {
        [SerializeField] public int DamageAmount;

        public int GetDamageValue()
        {
            return DamageAmount;
        }        
        
    }