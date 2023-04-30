using System;
using System.Collections;
using System.Collections.Generic;
using LudumDare53.Leveling;
using UnityEngine;

namespace LudumDare53.Boxes
{
    public class BoxOnConveyor : MonoBehaviour
    {
        private Rigidbody2D _rb2d;
        private Vector2 _startPoint;
        private Vector2 _endPoint ;
        private float _speed;
        private Vector2 _inertia;
        private bool _toDetach;
        private Vector2 _offset;

        public void Start()
        {
            _offset = Vector2.up * gameObject.GetComponent<Collider2D>().bounds.size.y / 2;
            _rb2d = GetComponentInChildren<Rigidbody2D>();
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

            var currentPos = transform.position.x * Vector2.right + // current position
                             Vector2.up * (_startPoint.y + _offset.y); //offset depending of box size;
            var destinationPos = _endPoint + _offset;

            var towardPosition = Vector2.MoveTowards(
                currentPos,
                destinationPos,
                _speed * Time.fixedDeltaTime);
            transform.position = towardPosition;
            var futurePosition = Vector2.MoveTowards(
                towardPosition,
                destinationPos,
                _speed * Time.fixedDeltaTime);
            Debug.Log($"{futurePosition}, {_endPoint + _offset}");
            if (futurePosition == _endPoint + _offset) DetachFromConveyor();
        }

        public void DetachFromConveyor()
        {
            if (PauseManager.IsPaused) return;
            _rb2d.bodyType = RigidbodyType2D.Dynamic;
            _toDetach = true;
        }

        public void Init(Vector2 startPoint, Vector2 endPoint, float speed)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
            _speed = speed;
            _inertia = (_endPoint - (Vector2)transform.position).normalized;
        }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawWireSphere(transform.position, 1);
        //     Gizmos.color = Color.green;
        //     Gizmos.DrawWireSphere(_endPoint + _offset, 1);
        // }
    }
}