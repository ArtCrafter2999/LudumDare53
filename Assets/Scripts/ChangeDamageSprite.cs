using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDamageSprite : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private List<DamageSprite> damageSprites;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxDamage>().onDamaged.AddListener(DamageChanged);
    }

    private void DamageChanged(float health)
    {
        damageSprites.Sort((a, b) => a.minDamage < b.minDamage? 1: -1);
        for (int i = 1; i < damageSprites.Count; i++)
        {
            if (damageSprites[i - 1].minDamage < health && health <= damageSprites[i].minDamage)
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
