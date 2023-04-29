using System;
using System.Collections;
using System.Collections.Generic;
using LudumDare53.Leveling;
using UnityEngine;

namespace LudumDare53.Boxes
{
    public class BoxOnConveyor : MonoBehaviour
    {
        private Transform _endPoint;
        private float _speed;
        private Rigidbody2D _rb2d;
        private Vector2 _inertia;
        private bool _toDetach;

        public void Start()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _rb2d.bodyType = RigidbodyType2D.Kinematic;
        }

        public void FixedUpdate()
        {
            if (PauseManager.IsPaused) return;
            if (_toDetach)
            {
                _rb2d.AddForce(_inertia * _speed, ForceMode2D.Impulse);
                enabled = false;
                return;
            }

            var torwardPosition =
                Vector3.MoveTowards(
                    transform.position,
                    _endPoint.position,
                    _speed * Time.fixedDeltaTime
                );
            transform.position = torwardPosition;
            if (Vector3.MoveTowards(
                    torwardPosition,
                    _endPoint.position,
                    _speed * Time.fixedDeltaTime
                )
                == _endPoint.position) DetachFromConveyor();
        }

        private void DetachFromConveyor()
        {
            if (PauseManager.IsPaused) return;
            _rb2d.bodyType = RigidbodyType2D.Dynamic;
            _toDetach = true;
        }

        public void Init(Transform endPoint, float speed)
        {
            _endPoint = endPoint;
            _speed = speed;
            _inertia = (_endPoint.position - transform.position).normalized;
        }
    }
}