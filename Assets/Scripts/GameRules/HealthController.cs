using LudumDare53.Leveling;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace LudumDare53.GameRules
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private float _maxHealth = 100;
        [SerializeField] private float _currentHealth;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private float _gameTimeInSeconds = 300;
        [SerializeField] private UIManager _uiManger;

        private float _decreaseRate;

        public UnityEvent HealthEnded;

        private void Start()
        {
            _currentHealth = _maxHealth;
            _healthSlider.maxValue = _maxHealth;
            _healthSlider.value = _currentHealth;

            _decreaseRate = _maxHealth / _gameTimeInSeconds;
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
            _currentHealth -= _decreaseRate * Time.deltaTime;
            _healthSlider.value = _currentHealth;

            if (_currentHealth <= 0)
            {
                _uiManger.YouAreFired();
                HealthEnded.Invoke();
                Debug.Log("Game Over");
            }
        }

        public void RestoreHealth(float healthAmount)
        {
            _currentHealth += healthAmount;
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
            _healthSlider.value = _currentHealth;
        }
    }

}