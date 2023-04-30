using System.Collections;
using System.Collections.Generic;
using LudumDare53.Leveling;
using UnityEngine;

namespace LudumDare53.Boxes
{
    public class BoxOnConveyor : MonoBehaviour
    {
        private Transform _wrapObject;
        private Rigidbody2D _rb2d;
        private Transform _endPoint;
        private float _speed;
        private Vector2 _inertia;
        private bool _toDetach;

        public void Start()
        {
            _rb2d = GetComponentInChildren<Rigidbody2D>();
            _rb2d.bodyType = RigidbodyType2D.Kinematic;
        }

        public void FixedUpdate()
        {
            if (PauseManager.IsPaused) return;
            if (_toDetach)
            {
                _rb2d.AddForce(_inertia * _speed, ForceMode2D.Impulse);
                Destroy(_wrapObject.gameObject);
                return;
            }

            var towardPosition =
                Vector3.MoveTowards(
                    _wrapObject.position,
                    _endPoint.position,
                    _speed * Time.fixedDeltaTime
                );
            transform.position = towardPosition;
            if (Vector3.MoveTowards(
                    towardPosition,
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
            _rb2d.transform.parent = _wrapObject.parent;
        }

        public void Init(Transform wrapObject,Transform endPoint, float speed)
        {
            _wrapObject = wrapObject;
            _endPoint = endPoint;
            _speed = speed;
            _inertia = (_endPoint.position - transform.position).normalized;
        }
    }
}