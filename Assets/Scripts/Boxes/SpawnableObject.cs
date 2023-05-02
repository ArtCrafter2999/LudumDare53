using System;
using DanPie.Framework.Randomnicity;
using UnityEngine;

namespace LudumDare53.Boxes
{
    [Serializable]
    public class SpawnableObject : IRandomSelectableItem
    {
        [SerializeField] private int _spawnChanse = 1;
        [SerializeField] private GameObject _prefab;

        public int SelectionChance { get => _spawnChanse; }
        public GameObject Prefab { get => _prefab; }
    }
}