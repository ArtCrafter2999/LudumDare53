using System;
using UnityEngine;

namespace LudumDare53.Truck
{
    [Serializable]
    public class TruckControllerDifficultyPresset
    {
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private float _truckSpawnDelay = 5f;

        public Transform[] SpawnPoints { get => _spawnPoints; }
        public float TruckSpawnDelay { get => _truckSpawnDelay; }
    }
}