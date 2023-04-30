using System;
using System.Collections.Generic;
using DG.Tweening;
using LudumDare53.Interactions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace LudumDare53.Boxes
{
    [RequireComponent(typeof(BoxDamage))]
    public class ChangeDamageSprite : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private List<DamageSprite> damageSprites;

        private BoxDamage _boxDamage;
        public UnityEvent disappeared;
        private float Health => _boxDamage.InterpolatedHealth;
        private void Start()
        {
            _boxDamage = GetComponent<BoxDamage>();
            _boxDamage.crushed.AddListener(Disappearing);
        }
        
        private void Disappearing()
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<DraggableObject>().enabled = false;
            GetComponent<Rigidbody2D>().isKinematic = true;
            disappeared.AddListener(() => Destroy(gameObject));
            GetComponent<SpriteRenderer>().DOFade(0, 5).OnComplete(disappeared.Invoke);
        }

        private void Update()
        {
            damageSprites.Sort((a, b) => a.minDamage < b.minDamage? 1: -1);
            for (int i = 0; i < damageSprites.Count; i++)
            {
                if ((i == 0 || damageSprites[i - 1].minDamage > Health) && Health >= damageSprites[i].minDamage)
                    spriteRenderer.sprite = damageSprites[i].sprite;
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
