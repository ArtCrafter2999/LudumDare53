using DanPie.Framework.Coroutines;
using DG.Tweening;
using LudumDare53.Boxes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LudumDare53.Truck
{
    public class Truck : MonoBehaviour
    {
        [SerializeField] private Collider2D _cargoCollider;
        [SerializeField] protected float _moveDuration = 5f;
        [SerializeField] private string _marker = "green";

        private bool _isFull = false;
        private bool _isMoving = false;
        private float _occupiedArea = 0f;
        private List<GameObject> _boxes = new();
        private float _maxArea => _cargoCollider.bounds.size.x * _cargoCollider.bounds.size.y;
        public Button GoButton => GetComponentInChildren<Button>();
        public string[] AvailableTruckColors => new string[] { "green", "blue", "red" };

        public string Marker
        {
            get => _marker; set
            {
                string lowerValue = value.ToLower();
                if (!AvailableTruckColors.Contains(lowerValue))
                {
                    throw new ArgumentException($"Invalid marker '{lowerValue}'. Allowed markers: {string.Join(", ", AvailableTruckColors)}");
                }
                _marker = lowerValue;
            }
        }


        /// <summary>
        /// Event that is triggered when the truck is full.
        /// </summary>
        public UnityEvent<Truck, List<GameObject>> TruckFull;
        /// <summary>
        /// Event that is triggered when a box is removed from a full truck
        /// </summary>
        public UnityEvent<Truck, List<GameObject>> TruckNotFull;

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
                if (other.TryGetComponent(out Tutorial.Outline boxOuntine))
                {
                    ToggleBoxOutlineIfMarkerMatches(boxOuntine, true);
                }

                if (other.TryGetComponent(out BoxCollider2D boxCollider))
                {
                    Vector2 boxSize = boxCollider.size;

                    boxSize.x *= other.transform.lossyScale.x;
                    boxSize.y *= other.transform.lossyScale.y;

                    float boxArea = Mathf.Abs(boxSize.x * boxSize.y);
                    Debug.Log($"Box entered. Area: {boxArea}");

                    _boxes.Add(other.gameObject);
                    _occupiedArea += boxArea;
                    _isFull = _occupiedArea >= _maxArea - 1;
                    if (_isFull)
                    {
                        TruckFull.Invoke(this, _boxes);
                        Debug.Log("Cargo is full");
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Box") && !_isMoving)
            {
                if (other.TryGetComponent(out Tutorial.Outline boxOuntine))
                {
                    ToggleBoxOutlineIfMarkerMatches(boxOuntine, false);
                }

                if (other.TryGetComponent(out BoxCollider2D boxCollider))
                {
                    Vector2 boxSize = boxCollider.size;

                    boxSize.x *= other.transform.lossyScale.x;
                    boxSize.y *= other.transform.lossyScale.y;

                    float boxArea = Mathf.Abs(boxSize.x * boxSize.y);

                    _boxes.Remove(other.gameObject);
                    _occupiedArea -= boxArea;

                    bool oldIsFull = _isFull;
                    _isFull = _occupiedArea >= _maxArea - 1;
                    Debug.Log($"Box exited. Area: {boxArea}");
                    if (!_isFull && oldIsFull != _isFull)
                        TruckNotFull.Invoke(this, _boxes);
                }
            }
        }

        private void ToggleBoxOutlineIfMarkerMatches(Tutorial.Outline outline, bool enabled)
        {
            if (outline.TryGetComponent(out BoxMarker marker) && !string.Equals(marker.type, Marker, StringComparison.OrdinalIgnoreCase))
            {
                if (enabled)
                {
                    outline.Activate();
                }
                else
                {
                    outline.Deactivate();
                }
            }
        }

        private void DissableBoxesRigidbody()
        {
            _isMoving = true;
            foreach (GameObject box in _boxes)
            {
                Rigidbody2D rb = box.GetComponent<Rigidbody2D>();
                Collider2D bc = box.GetComponent<Collider2D>();
                rb.bodyType = RigidbodyType2D.Kinematic;
                bc.enabled = false;

                box.transform.SetParent(transform);
            }
            ChangeTruckColliders(false);
        }

        private void ChangeTruckColliders(bool enabled)
        {
            Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
            foreach (Collider2D collider in colliders)
            {
                collider.enabled = enabled;
            }
        }
    }
}
