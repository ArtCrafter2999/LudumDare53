using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class BoxDamage : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float fragilityLevel;
    
    private float _health;
    public float InterpolatedHealth => _health / maxHealth;

    public UnityEvent<float> onDamaged;

    private void Start()
    {
        _health = maxHealth;
        onDamaged.AddListener(f => Debug.Log($"more than fragility, health: ${f}%"));
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log($"velocity: ${col.relativeVelocity}, magnitude: ${col.relativeVelocity.magnitude}");
        if (col.relativeVelocity.magnitude >= fragilityLevel)
        {
            _health = Mathf.Clamp(_health - col.relativeVelocity.magnitude, 0, maxHealth);
            onDamaged.Invoke(InterpolatedHealth);
        } 
    }
}
