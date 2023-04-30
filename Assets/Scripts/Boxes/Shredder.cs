using System;
using UnityEngine;

namespace LudumDare53.Boxes
{
    public class Shredder : MonoBehaviour
    {
        [SerializeField] private float power;
        [SerializeField] private float damage;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out BoxDamage boxDamage)) return;
            collision.rigidbody.AddForce(Vector2.up*power, ForceMode2D.Impulse);
            boxDamage.Damage(damage);
        }
    }
}