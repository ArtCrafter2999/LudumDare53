using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LudumDare53.GameRules;
using LudumDare53.Interactions;
using UnityEngine;
using UnityEngine.Events;
using Color = System.Drawing.Color;

namespace LudumDare53.Boxes
{
    public class BoxDamage : MonoBehaviour
    {
        public float reducingPointsFactor = -1f;
        public float damage;
        [SerializeField]
        public float maxHealth;
        [SerializeField]
        private float durabilityLevel;
        public UnityEvent<float> damaged;
        public UnityEvent crushed;
        private ReduceablePoints _redusablePoints;
        private float _health;
        public float InterpolatedHealth => _health / maxHealth;

        private void Start()
        {
            _redusablePoints = GameObject.FindAnyObjectByType<ReduceablePoints>().GetComponent<ReduceablePoints>();
            _health = maxHealth;
        }

        public void Damage(float count)
        {
            _redusablePoints.ChangeHealth(reducingPointsFactor);
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