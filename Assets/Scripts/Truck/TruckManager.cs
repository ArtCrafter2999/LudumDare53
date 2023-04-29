using DanPie.Framework.Coroutines;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare53.Truck
{
    public class TruckManager : MonoBehaviour
    {
        [SerializeField] private int _maxTrucksCount = 2;
        [SerializeField] private Truck _truckPrefab;
        [SerializeField] private float _truckSpawnDelay = 5f;
        [SerializeField] private Transform _trucksSpawnPoint;

        private List<Truck> _trucks = new();

        private void Start()
        {
            for (int i = 0; i < _maxTrucksCount; i++)
            {
                CreateTruck((i + 1) * 10);
            }
        }

        private void CreateTruck(float position)
        {
            Truck truck = Instantiate(_truckPrefab, transform.position, Quaternion.identity);
            truck.transform.SetParent(_trucksSpawnPoint);

            //  _trucks.Add(truck);
            truck.MoveTo(truck.transform.position.x - position);
            truck.TruckFull.AddListener(() =>
            {
                truck.MoveTo(truck.transform.position.x - position * 5);
                StartCoroutine(CoroutineUtilities.WaitForSeconds(_truckSpawnDelay, () =>
                {
                    Destroy(truck.gameObject);
                    CreateTruck(position);
                }));
            });
        }

    }
}