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

            truck.MoveTo(truck.transform.position.x - _moveDistance);
            return truck;
        }

        public void RemoveTruck(Truck truck)
        {
            StartCoroutine(CoroutineUtilities.WaitForSeconds(3f, () =>
                Destroy(truck.gameObject)));
        }
    }
}