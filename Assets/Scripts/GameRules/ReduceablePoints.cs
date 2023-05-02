using System;
using LudumDare53.Leveling;
using UnityEngine;
using UnityEngine.Events;

namespace LudumDare53.GameRules
{
    public class ReduceablePoints : MonoBehaviour
    {
        [SerializeField] private float _maxPoints = 100;
        [Tooltip("Determines how many points will decrease in one second.")]
        public float decreaseRate;
        public float frequency;
        private float _cooldown;

        public UnityEvent<float> PointsChanged;

        public float CurrentPoints { get => _currentPoints; }

        private void Start()
        {
            CurrentPoints = _maxPoints;
            _cooldown = frequency;
        }

        private void OnEnable()
        {
            DifficultyManager.DifficultyChanged.AddListener(OnDifficultyChanged);
        }

        private void OnDisable()
        {
            DifficultyManager.DifficultyChanged.RemoveListener(OnDifficultyChanged);
        }

        private void OnDifficultyChanged()
        {
            CurrentPoints = _maxPoints;
        }

        private void Update()
        {
            if (PauseManager.IsPaused) return;
            if (_cooldown > 0)
                _cooldown -= Time.deltaTime;
            else
                RestoreHealth(-decreaseRate);
        }

        private void DecreaseHealth()
        {
            _currentPoints -= decreaseRate * Time.deltaTime;
            _currentPoints = Mathf.Max(0, _currentPoints);
            PointsChanged.Invoke(_currentPoints * Time.deltaTime);
        }

        public void RestoreHealth(float healthAmount)
        {
            _currentPoints += healthAmount;
            _currentPoints = Mathf.Clamp(_currentPoints, 0, _maxPoints);
            PointsChanged.Invoke(healthAmount);
            _cooldown = frequency;
        }
    }

}