using DanPie.Framework.Coroutines;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LudumDare53.Truck
{
    public class Truck : MonoBehaviour
    {
        [SerializeField] private Collider2D _cargoCollider;
        [SerializeField] protected float _moveDuration = 5f;

        private bool _isFull = false;
        private bool _isMoving = false;
        private float _occupiedArea = 0f;
        private List<GameObject> _boxes = new();
        private float _maxArea => _cargoCollider.bounds.size.x * _cargoCollider.bounds.size.y;
        public Button GoButton => GetComponentInChildren<Button>();

        /// <summary>
        /// Event that is triggered when the truck is full.
        /// </summary>
        public UnityEvent<Truck, List<GameObject>> TruckFull;

        public void MoveTo(float distance)
        {
            DissableBoxesRigidbody();
            _cargoCollider.isTrigger = false;
            transform.DOMoveX(distance, _moveDuration);
            StartCoroutine(CoroutineUtilities.WaitForSeconds(_moveDuration, () =>
           {
               _cargoCollider.isTrigger = true;
               _isMoving = false;
               ChangeTruckColliders(true);
           }));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Box") && !_isMoving)
            {
                float boxArea = other.bounds.size.x * other.bounds.size.y;
                _boxes.Add(other.gameObject);
                _occupiedArea += boxArea;
                _isFull = _occupiedArea >= _maxArea - 1;

                if (_isFull)
                {
                    TruckFull.Invoke(this, _boxes);
                    Debug.Log("Cargo is full");
                }
                Debug.Log($"boxArea {boxArea}");
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Box") && !_isMoving)
            {
                float boxArea = other.bounds.size.x * other.bounds.size.y;
                _boxes.Remove(other.gameObject);
                _occupiedArea -= boxArea;
                Debug.Log($"Box out");
            }
        }
        private void DissableBoxesRigidbody()
        {
            _isMoving = true;
            foreach (GameObject box in _boxes)
            {
                var rb = box.GetComponent<Rigidbody2D>();
                var bc = box.GetComponent<Collider2D>();
                rb.bodyType = RigidbodyType2D.Kinematic;
                bc.enabled = false;

                box.transform.SetParent(transform);
            }
            ChangeTruckColliders(false);
        }

        private void ChangeTruckColliders(bool enabled)
        {
            Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
            foreach (var collider in colliders)
            {
                collider.enabled = enabled;
            }
        }
    }
}
