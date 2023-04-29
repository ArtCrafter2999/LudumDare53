using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LudumDare53.Boxes
{
    public class BoxDamage : MonoBehaviour
    {
        [SerializeField]
        public float maxHealth;
        [SerializeField]
        private float durabilityLevel;
        public UnityEvent<float> damaged;
    
        private float _health;
        public float InterpolatedHealth => _health / maxHealth;

        private void Start()
        {
            _health = maxHealth;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.relativeVelocity.magnitude >= durabilityLevel)
            {
                _health = Mathf.Clamp(_health - col.relativeVelocity.magnitude, 0, maxHealth);
                damaged.Invoke(InterpolatedHealth);
            } 
        }
    }
}