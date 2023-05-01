using System.Collections.Generic;
using DanPie.Framework.Randomnicity;
using UnityEngine;

namespace LudumDare53.Boxes
{
    [CreateAssetMenu(fileName = "ConveyorDifficultyPresset", menuName = "Difficulty/ConveyorDifficultyPresset")]
    public class ConveyorDifficultyPresset : ScriptableObject
    {
        [SerializeField] private float _firstBoxSpawnDelay = 1f;
        [SerializeField] private float _speed = 4f;
        [SerializeField] private float _period = 4f;
        [SerializeField] private List<SpawnableObject> _spawnableObjects;

        public float FirstBoxSpawnDelay { get => _firstBoxSpawnDelay; }
        public float Speed { get => _speed; }
        public float Period { get => _period; }
        public List<SpawnableObject> SpawnableObjects { get => _spawnableObjects; }
    }
}