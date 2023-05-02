using DanPie.Framework.Coroutines;
using LudumDare53.Leveling;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LudumDare53.Truck
{
    public class TruckController : MonoBehaviour
    {
        [SerializeField] private TruckFactory _truckFactory;
        [SerializeField] private List<TruckControllerDifficultyPresset> _pressets;

        private TruckControllerDifficultyPresset _currentPresset;
        private List<Truck> _trucks = new();
        private Queue<Transform> _freePositions = new();
        private int _truckCount = 3;
        public List<Truck> ActiveTrucks => _trucks;
        /// <summary>
        /// Event that is triggered when the truck count changes.
        /// </summary>
        public UnityEvent<int, int> TruckCountChanged;
        private bool _isAlreadyChanged;

        public int TruckCount
        {
            get => _truckCount; set
            {
                if (value < 1 || value > 3)
                    throw new ArgumentOutOfRangeException($"{nameof(TruckCount)} must be between 1 and 3");

                _truckCount = value;
            }
        }

        public void RecreateTrucks()
        {
            foreach (var item in _trucks)
            {
                if (item != null)
                {
                    Destroy(item.gameObject);
                }
            }

            _trucks = new();
            _currentPresset = _pressets[Mathf.Clamp(DifficultyManager.Difficulty, 0, _pressets.Count - 1)];
            TruckCount = _currentPresset.SpawnPoints.Length;
            _freePositions = new(_currentPresset.SpawnPoints);
            for (int i = 0; i < TruckCount; i++)
            {
                AddTruck();
            }
        }


        private void CreateTruck()
        {
            Transform position = _freePositions.Dequeue();
            Truck truck = _truckFactory.CreateTruck(position);
            _trucks.Add(truck);

            truck.TruckLeft.AddListener((truck, _) =>
            {
                RemoveTruck(truck);
                truck.GetComponentInChildren<Canvas>().enabled = false;
                StartCoroutine(CoroutineUtilities.WaitForSeconds(_currentPresset.TruckSpawnDelay, AddTruck));
            });

        }

        public void AddTruck()
        {
            CreateTruck();
            TruckCountChanged.Invoke(0, 0);
        }

        private void RemoveTruck(Truck truck)
        {
            _freePositions.Enqueue(truck.transform.parent);
            _truckFactory.RemoveTruck(truck);
            _trucks.Remove(truck);
        }
    }
}