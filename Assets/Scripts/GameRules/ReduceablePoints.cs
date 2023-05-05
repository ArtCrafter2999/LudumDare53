using System;
using LudumDare53.Leveling;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace LudumDare53.GameRules
{
    public class ReduceablePoints : MonoBehaviour
    {
        [SerializeField] private float _maxPoints = 100;

        [Tooltip("Determines how many points will decrease in one second.")]
        public float decreaseRate;

        public float frequency;
        private float _cooldown;

        public UnityEvent<float> pointsChanged;
        public UnityEvent<float> pointsNaturallyChanged;

        public float CurrentPoints { get; private set; }

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
            {
                SilentChangeHealth(-decreaseRate);
                pointsNaturallyChanged.Invoke(-decreaseRate);
            }
                
        }

        public void ChangeHealth(float healthAmount)
        {
            SilentChangeHealth(healthAmount);
            pointsChanged.Invoke(healthAmount);
        }
        public void SilentChangeHealth(float healthAmount)
        {
            CurrentPoints += healthAmount;
            CurrentPoints = Mathf.Clamp(CurrentPoints, 0, _maxPoints);
            _cooldown = frequency;
        }
    }

}