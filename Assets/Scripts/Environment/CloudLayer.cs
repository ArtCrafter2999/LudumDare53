using System;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare53.Environment
{
    [Serializable]
    public class CloudLayer
    {
        public List<SpriteRenderer> CloudsOnLayer;
        public float LayerSpeed = 10f;
        [Min(0)]
        public float GenerationFrequency = 3f;
        [Min(0)]
        public float YPositinonScatter = 2f;
        [Min(0)]
        public float ScaleScatter = 0.3f;
        public Vector3 InitialScale = Vector3.one;
        public Transform SpawnEdge;
        public Transform DestroyEdge;

        [HideInInspector]
        public float GenerationCooldown = 0f;
        [HideInInspector]
        public int PreviousCloudId = -1;
    }
}
