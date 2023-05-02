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

        public UnityEvent<float, float> PointsChanged;

        public float CurrentPoints { get; private set; }

        private void Start()
        {
            CurrentPoints = _maxPoints;
            _cooldown = frequency;
        }

        private void Update()
        {
            if (PauseManager.IsPaused) return; 
            if(_cooldown >0)
                _cooldown -= Time.deltaTime;
            else
                RestoreHealth(-decreaseRate);
        }

        private void DecreaseHealth()
        {
            CurrentPoints -= decreaseRate * Time.deltaTime;
            CurrentPoints = Mathf.Max(0, CurrentPoints);
            PointsChanged.Invoke(CurrentPoints, CurrentPoints * Time.deltaTime);
        }

        public void RestoreHealth(float healthAmount)
        {
            CurrentPoints += healthAmount;
            CurrentPoints = Mathf.Clamp(CurrentPoints, 0, _maxPoints);
            PointsChanged.Invoke(CurrentPoints, healthAmount);
            _cooldown = frequency;
        }
    }

}