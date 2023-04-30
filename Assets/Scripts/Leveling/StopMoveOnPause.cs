using System;
using UnityEngine;

namespace LudumDare53.Leveling
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class StopMoveOnPause : MonoBehaviour
    {
        private Rigidbody2D _rb2d;
        private RigidbodyType2D _savedBodyType;
        private Vector2 _savedVelocity;
        private float _savedAngularVelocity;
        public void Start()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            PauseManager.Pause += OnPause;
            PauseManager.Resume += OnResume;
        }

        private void OnPause()
        {
            (_savedBodyType, _rb2d.bodyType) = (_rb2d.bodyType, RigidbodyType2D.Kinematic);
            (_savedVelocity, _rb2d.velocity) = (_rb2d.velocity,Vector2.zero);
            (_savedAngularVelocity, _rb2d.angularVelocity) = (_rb2d.angularVelocity, 0);
        }

        private void OnResume()
        {
            _rb2d.bodyType = _savedBodyType;
            _rb2d.velocity = _savedVelocity;
            _rb2d.angularVelocity = _savedAngularVelocity;
        }
    }
}