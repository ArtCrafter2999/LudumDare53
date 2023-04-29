using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare53.Truck
{
    public class Truck : MonoBehaviour
    {
        [SerializeField] private Collider2D _cargoCollider;
        [SerializeField] private float _moveSpeed;
        [SerializeField] protected float _moveDistance = 50f;
        [SerializeField] protected float _moveDuration = 5f;

        private bool _isFull = false;
        private float _occupiedArea = 0f;
        private float _maxArea;
        private List<GameObject> _boxes;

        private void Start()
        {
            _maxArea = _cargoCollider.bounds.size.x * _cargoCollider.bounds.size.y;
            _boxes = new List<GameObject>();
            MoveIn();
        }

        public void MoveOut()
        {
            DissableBoxesRigidbody();
            Move(transform.position.x - _moveDistance);
        }

        public void MoveIn()
        {
            Move(transform.position.x + _moveDistance);
        }

        private void Move(float distance)
        {
            transform.DOMoveX(distance, _moveDuration);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Box"))
            {
                float boxArea = other.bounds.size.x * other.bounds.size.y;
                _boxes.Add(other.gameObject);
                _occupiedArea += boxArea;
                _isFull = _occupiedArea >= _maxArea - 1;

                if (_isFull)
                {
                    Debug.Log("Cargo is full");
                }
            }
        }

        private void DissableBoxesRigidbody()
        {
            foreach (GameObject box in _boxes)
            {
                Destroy(box.GetComponent<Rigidbody2D>());
                box.transform.SetParent(transform);
            }
        }
    }
}
