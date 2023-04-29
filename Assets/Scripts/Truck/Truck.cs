using UnityEngine;

namespace LudumDare53.Truck
{
    public class Truck : MonoBehaviour
    {
        [SerializeField] private Collider2D _cargoCollider;

        private bool _isFull = false;
        private float _occupiedArea = 0f;
        private float _maxArea;

        void Start()
        {
            _maxArea = _cargoCollider.bounds.size.x * _cargoCollider.bounds.size.y;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Box"))
            {
                float boxArea = other.bounds.size.x * other.bounds.size.y;

                _occupiedArea += boxArea;
                _isFull = _occupiedArea >= _maxArea - 1;

                if (_isFull)
                {
                    Debug.Log("Cargo is full");
                }
                Debug.Log("Occupied area: " + _occupiedArea);
            }
        }
    }
}
