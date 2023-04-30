using LudumDare53.Leveling;
using UnityEngine;
using UnityEngine.Events;

namespace LudumDare53.GameRules
{
    public class ReduceablePoints : MonoBehaviour
    {
        [SerializeField] private float _maxPoints = 100;
        [Tooltip("Determines how many points will decrease in one second.")]
        [SerializeField] private float _decreaseRate;    

        private float _currentPoints;

        public UnityEvent<float> PointsChanged;

        public float CurrentPoints { get => _currentPoints; }

        private void Start()
        {
            _currentPoints = _maxPoints;
        }

        private void Update()
        {
            if (!PauseManager.IsPaused)
            {
                DecreaseHealth();
            }
        }

        private void DecreaseHealth()
        {
            _currentPoints -= _decreaseRate * Time.deltaTime;
            _currentPoints = Mathf.Max(0, _currentPoints);
            PointsChanged.Invoke(_currentPoints);
        }

        public void RestoreHealth(float healthAmount)
        {
            _currentPoints += healthAmount;
            _currentPoints = Mathf.Min(_currentPoints, _maxPoints);
        }
    }

}