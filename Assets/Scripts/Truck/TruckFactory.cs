using DanPie.Framework.Coroutines;
using UnityEngine;

namespace LudumDare53.Truck
{
    public class TruckFactory : MonoBehaviour
    {
        [SerializeField] private Truck _truckPrefab;
        [SerializeField] private float _moveDistance;
        public Truck CreateTruck(Transform position)
        {
            Truck truck = Instantiate(_truckPrefab, position.position, Quaternion.identity);
            truck.transform.SetParent(position);
            AddButtons(truck);

            truck.MoveTo(truck.transform.position.x - _moveDistance);
            return truck;
        }

        private void AddButtons(Truck truck)
        {
            Canvas canvas = truck.GetComponentInChildren<Canvas>();

            RectTransform buttonRectTransform = truck.GoButton.gameObject.GetComponent<RectTransform>();

            Vector3 worldButtonPosition = truck.transform.TransformPoint(new Vector3(-40f, 3.97f, 0));
            Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, worldButtonPosition);

            RectTransform canvasRect = canvas.gameObject.GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPosition,
                canvas.worldCamera, out Vector2 localButtonPosition);

            buttonRectTransform.localPosition = localButtonPosition;
            buttonRectTransform.localScale = Vector3.one;

            canvas.enabled = false;
            truck.TruckFull.AddListener((truck, boxes) => canvas.enabled = true);
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