using DanPie.Framework.Coroutines;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LudumDare53.Truck
{

    public class TruckController : MonoBehaviour
    {
        [SerializeField] private TruckFactory _truckFactory;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private List<Truck> _trucks = new();
        [SerializeField] private float _truckSpawnDelay = 5f;

        private Queue<Transform> _freePositions = new();
        private int _truckCount = 3;
        /// <summary>
        /// Event that is triggered when the truck count changes.
        /// </summary>
        public UnityEvent<int, int> TruckCountChanged;
        public int TruckCount
        {
            get => _truckCount; set
            {
                if (value < 1 || value > 3)
                    throw new ArgumentOutOfRangeException($"{nameof(TruckCount)} must be between 1 and 3");

                _truckCount = value;
            }
        }

        private void Start()
        {
            _freePositions = new(_spawnPoints);
            for (int i = 0; i < TruckCount; i++)
            {
                CreateTruck();
            }
        }

        private void CreateTruck()
        {
            Transform position = _freePositions.Dequeue();
            Truck truck = _truckFactory.CreateTruck(position);
            _trucks.Add(truck);

            truck.GoButton.onClick.AddListener(() =>
              {
                  RemoveTruck(truck);
                  truck.GetComponentInChildren<Canvas>().enabled = false;
                  StartCoroutine(CoroutineUtilities.WaitForSeconds(_truckSpawnDelay, CreateTruck));
              });

        }

        public void AddTruck()
        {
            CreateTruck();
            TruckCountChanged.Invoke(TruckCount++, TruckCount);
        }

        private void RemoveTruck(Truck truck)
        {
            _freePositions.Enqueue(truck.transform.parent);
            _truckFactory.RemoveTruck(truck);
            _trucks.Remove(truck);
        }
    }
}