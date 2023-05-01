using UnityEngine;

namespace LudumDare53.Tutorial
{
    public class PrefabSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _spawnPoint;

        public GameObject Spawn()
        {
            var result = Instantiate(_prefab, _spawnPoint);
            result.transform.parent = null;
            return result;
        }
    }
}
