using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LudumDare53.Boxes
{
    public class BoxMarker : MonoBehaviour
    {
        public int BoxMarkerIndex { get; private set; }

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private List<Sprite> markersSprites;

        public void Start()
        {
            BoxMarkerIndex = Random.Range(0, markersSprites.Count);
            spriteRenderer.sprite = markersSprites[BoxMarkerIndex];
        }
    }
}