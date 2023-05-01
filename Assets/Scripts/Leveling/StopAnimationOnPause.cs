using System;
using UnityEngine;

namespace LudumDare53.Leveling
{
    [RequireComponent(typeof(Animator))]
    public class StopAnimationOnPause : MonoBehaviour
    {
        private Animator _animator;
        private float _savedSpeed;

        public void Start()
        {
            _animator = GetComponent<Animator>();
            PauseManager.Pause.AddListener(OnPause);
            PauseManager.Resume.AddListener(OnResume);
            if (PauseManager.IsPaused) OnPause();
            else OnResume();
        }

        private void OnPause()
        {
            if (!this.IsOnPause()) return;
            (_savedSpeed, _animator.speed) = (_animator.speed, 0);
        }

        private void OnResume()
        {
            _animator.speed = _savedSpeed;
        }
    }
}