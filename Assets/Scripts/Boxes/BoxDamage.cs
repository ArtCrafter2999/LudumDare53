using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LudumDare53.Interactions;
using UnityEngine;
using UnityEngine.Events;
using Color = System.Drawing.Color;

namespace LudumDare53.Boxes
{
    public class BoxDamage : MonoBehaviour
    {
        [SerializeField]
        public float maxHealth;
        [SerializeField]
        private float durabilityLevel;
        public UnityEvent<float> damaged;
        public UnityEvent crushed;

        private float _health;
        public float InterpolatedHealth => _health / maxHealth;

        private void Start()
        {
            _health = maxHealth;
        }

        public void Damage(float count)
        {
            _health = Mathf.Clamp(_health - count, 0, maxHealth);
            damaged.Invoke(InterpolatedHealth);
            if(_health<=0)crushed.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.relativeVelocity.magnitude >= durabilityLevel)
                Damage(col.relativeVelocity.magnitude);
            
        }
    }
}