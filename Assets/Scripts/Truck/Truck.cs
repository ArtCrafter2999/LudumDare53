using DanPie.Framework.Coroutines;
using DG.Tweening;
using LudumDare53.Boxes;
using LudumDare53.GameRules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DanPie.Framework.AudioManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LudumDare53.Truck
{
    public class Truck : MonoBehaviour
    {
        [SerializeField] private Collider2D _cargoCollider;
        [SerializeField] protected float _reward = 30f;
        [SerializeField] protected float _moveDuration = 5f;
        [SerializeField] protected float _timeBeforeLeft = 2f;
        [SerializeField] protected int _spriteRendererOrderOffset = 5;
        [SerializeField] private float _moveDistance;
        [SerializeField] private string _marker = "green";

        [Header("Sounds")] 
        [SerializeField] private AudioClipDataProvider arriveSound;
        [SerializeField] private AudioClipDataProvider leaveSound;
        
        private AudioSourcesManager _manager;

        private bool _isFull = false;
        private bool _isMoving = false;
        private float _occupiedArea = 0f;
        private List<GameObject> _boxes = new();
        private float _maxArea => _cargoCollider.bounds.size.x * _cargoCollider.bounds.size.y;
        public UnityEngine.UI.Button GoButton => GetComponentInChildren<Button>();
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

        private List<BoxMarker> _markedList = new List<BoxMarker>();

        /// <summary>
        /// Event that is triggered when the truck is full.
        /// </summary>
        public UnityEvent<Truck, List<GameObject>> TruckFull;
        /// <summary>
        /// Event that is triggered when a box is removed from a full truck
        /// </summary>
        public UnityEvent<Truck, List<GameObject>> TruckNotFull;
        public UnityEvent<Truck, List<GameObject>> TruckLeft;
        public UnityEvent WrongBoxComes;
        private int _orderChanges;
        private ReduceablePoints _redusablePoints;

        private void OnEnable()
        {
            _manager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSourcesManager>();
        }

        private void Start()
        {
            _redusablePoints = GameObject.FindAnyObjectByType<ReduceablePoints>().GetComponent<ReduceablePoints>();
            Canvas canvas = GetComponentInChildren<Canvas>();
            canvas.enabled = false;
            TruckFull.AddListener((truck, boxes) => canvas.enabled = true);
            TruckNotFull.AddListener((truck, boxes) => canvas.enabled = false);

            GoButton.onClick.AddListener(() => _manager.GetAudioSourceController().Play(leaveSound.GetClipData()));
            GoButton.onClick.AddListener(() => StartCoroutine(CoroutineUtilities.WaitForSeconds(_timeBeforeLeft, () =>
            {
                GoButton.GetComponent<Image>().enabled = false;
                MoveTo(transform.position.x - _moveDistance);
                GoToBackground();
                _redusablePoints.RestoreHealth(_reward);
                StartCoroutine(CoroutineUtilities.WaitForSeconds(_moveDuration, () => TruckLeft.Invoke(this, _boxes)));
            })));
            WrongBoxComes.AddListener(() => canvas.enabled = false);
        }

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
               GoToBackground();
           }));
        }



        public void GoToBackground()
        {
            foreach (var item in GetComponentsInChildren<SpriteRenderer>())
            {
                item.sortingOrder -= _spriteRendererOrderOffset;
            }
        }

        public void GoToFront()
        {
            _manager.GetAudioSourceController().Play(arriveSound.GetClipData());

            foreach (var item in GetComponentsInChildren<SpriteRenderer>())
            {
                item.sortingOrder += _spriteRendererOrderOffset;
            }
        }

        private void RemoveFromMarkeredList(BoxMarker marker)
        {
            if (_markedList.Contains(marker))
            {
                _markedList.Remove(marker);
            }
        }

        private void AddToMarkeredList(BoxMarker marker)
        {
            if (!_markedList.Contains(marker))
            {
                _markedList.Add(marker);
            }
        }

        private bool IsHaveBadMarker()
        {
            foreach (var item in _markedList)
            {
                if (item.type != Marker)
                {
                    return true;
                }
            }
            return false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Box") && !_isMoving)
            {
                MarkerStuff(other, true);

                if (other.TryGetComponent(out BoxCollider2D boxCollider))
                {
                    Vector2 boxSize = boxCollider.size;

                    boxSize.x *= other.transform.lossyScale.x;
                    boxSize.y *= other.transform.lossyScale.y;

                    float boxArea = Mathf.Abs(boxSize.x * boxSize.y);
                    //Debug.Log($"Box entered. Area: {boxArea}");

                    _boxes.Add(other.gameObject);
                    _occupiedArea += boxArea;
                    _isFull = _occupiedArea >= _maxArea - 1;

                    if (IsHaveBadMarker())
                    {
                        return;
                    }

                    if (_isFull)
                    {
                        TruckFull.Invoke(this, _boxes);
                        //Debug.Log("Cargo is full");
                    }
                }
            }
        }

        private void MarkerStuff(Collider2D other, bool enabled)
        {
            if (other.TryGetComponent(out BoxMarker marker))
            {
                if (enabled)
                {
                    AddToMarkeredList(marker);
                }
                else
                {
                    RemoveFromMarkeredList(marker);
                }
            }

            if (other.TryGetComponent(out Tutorial.Outline boxOuntine))
            {
                ToggleBoxOutlineIfMarkerMatches(boxOuntine, enabled);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Box") && !_isMoving)
            {

                MarkerStuff(other, false);

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
                    //Debug.Log($"Box exited. Area: {boxArea}");
                    if (IsHaveBadMarker())
                    {
                        return;
                    }

                    if (!_isFull && oldIsFull != _isFull)
                    {
                        TruckNotFull.Invoke(this, _boxes);
                    }

                    if (_isFull)
                    {
                        TruckFull.Invoke(this, _boxes);
                    }
                }
            }
        }

        private void ToggleBoxOutlineIfMarkerMatches(Tutorial.Outline outline, bool enabled)
        {
            if (outline.TryGetComponent(out BoxMarker marker) &&
                !string.Equals(marker.type, Marker, StringComparison.OrdinalIgnoreCase))
            {
                WrongBoxComes.Invoke();
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
            _boxes = _boxes.Where(x => x != null).ToList();
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
