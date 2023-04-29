using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LudumDare53.Boxes
{
    [RequireComponent(typeof(BoxDamage))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ChangeDamageSprite : MonoBehaviour
    {
        [SerializeField]
        private List<DamageSprite> damageSprites;


        private SpriteRenderer _spriteRenderer;
        private BoxDamage _boxDamage;
        private float Health => _boxDamage.InterpolatedHealth;
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxDamage = GetComponent<BoxDamage>();
        }

        private void Update()
        {
            damageSprites.Sort((a, b) => a.minDamage < b.minDamage? 1: -1);
            for (int i = 0; i < damageSprites.Count; i++)
            {
                if ((i == 0 || damageSprites[i - 1].minDamage > Health) && Health >= damageSprites[i].minDamage)
                    _spriteRenderer.sprite = damageSprites[i].sprite;
            }
        }
    
        [Serializable]
        public class DamageSprite
        {
            public float minDamage;
            public Sprite sprite;
        }
    }
}
