using System;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace LudumDare53.Tutorial
{
    [Serializable]
    public class PrefabSpawner
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform spawnPoint;

        public GameObject Spawn()
        {
            var result = Object.Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            result.transform.parent = null;
            return result;
        }
    }
}
