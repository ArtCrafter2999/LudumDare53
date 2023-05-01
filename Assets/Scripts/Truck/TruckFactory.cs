using DanPie.Framework.Coroutines;
using UnityEngine;

namespace LudumDare53.Truck
{
    public class TruckFactory : MonoBehaviour
    {
        [SerializeField] private Truck[] _truckPrefabs;
        [SerializeField] private float _moveDistance;
        public Truck CreateTruck(Transform position)
        {
            Truck truckPrefab = _truckPrefabs[Random.Range(0, _truckPrefabs.Length)];

            Truck truck = Instantiate(truckPrefab, position.position, Quaternion.identity);
            truck.transform.SetParent(position);


            Canvas canvas = truck.GetComponentInChildren<Canvas>();
            canvas.enabled = false;
            truck.TruckFull.AddListener((truck, boxes) => canvas.enabled = true);
            truck.TruckNotFull.AddListener((truck, boxes) => canvas.enabled = false);

            truck.MoveTo(truck.transform.position.x - _moveDistance);
            return truck;
        }

        public void RemoveTruck(Truck truck)
        {
            StartCoroutine(CoroutineUtilities.WaitForSeconds(2f, () =>
            truck.MoveTo(truck.transform.position.x - _moveDistance * 2)));

            StartCoroutine(CoroutineUtilities.WaitForSeconds(3f, () =>
                Destroy(truck.gameObject)));
        }
    }
}