using System;
using System.Collections.Generic;
using LudumDare53.Core;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace LudumDare53.GameRules
{
    public class ReduceablePointsView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ReduceablePoints points;
        [SerializeField] private List<Rate> rates;

        private void Update()
        {
            rates.Sort((a, b) => a.from < b.from? 1: -1);
            for (int i = 0; i < rates.Count; i++)
            {
                if ((i == 0 || rates[i - 1].from > points.CurrentPoints) && points.CurrentPoints >= rates[i].from)
                    spriteRenderer.sprite = rates[i].sprite;
            }
        }
    
        [Serializable]
        public class Rate
        {
            public float from;
            public Sprite sprite;
        }
    }
}