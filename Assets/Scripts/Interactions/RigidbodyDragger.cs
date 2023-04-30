using UnityEngine;
using SimpleHeirs;
using System;

namespace LudumDare53.Interactions
{
    public class RigidbodyDragger : MonoBehaviour
    {
        [SerializeField] private HeirsProvider<IMouseDragEventsProvider> _mouseDragEventsProvider;
        [SerializeField] private HeirsProvider<IMousePressingEventsProvider> _mousePressingEventsProvider;
        [Min(0)]
        [SerializeField] private float _moveSpeed;
        [Tooltip("Determines the degree to which the mass will affect the move speed of the object.")]
        [SerializeField] private float _massFactor = 1f;
        [SerializeField] private Rigidbody2D _pointer;

        private Camera _camera;
        private IMouseDragEventsProvider _mouseDragEventsProviderValue;
        private IMousePressingEventsProvider _mousePressingEventsProviderValue;
        private float _previousGravityScale;
        private CollisionDetectionMode2D _previousCollisionDetectionMode;
        private Vector2 _initialOffset;
        private DraggableObject _draggableObject;

        protected void OnEnable()
        {
            ResetTarget();
            _camera = Camera.main;
            _mouseDragEventsProviderValue = _mouseDragEventsProvider.GetValue();
            _mousePressingEventsProviderValue = _mousePressingEventsProvider.GetValue();

            _mouseDragEventsProviderValue.Dragged += Drag;
            _mouseDragEventsProviderValue.DraggingStarted += OnDraggingStarted;
            _mouseDragEventsProviderValue.DraggingStopped += OnDraggingStopped;
            _mousePressingEventsProviderValue.MouseDown += MouseDown;
        }

        protected void OnDisable()
        {
            _mouseDragEventsProviderValue.DraggingStarted -= MouseDown;
            _mouseDragEventsProviderValue.Dragged -= Drag;
        }

        private void OnDraggingStopped(Vector2 point)
        {
            if (_draggableObject != null)
            {
                Vector2 direction = GetTargetDirection(point);
                float magnitude = Mathf.Min(
                    GetSpeed(_draggableObject.Rigidbody2D), 
                    direction.magnitude);

                _draggableObject.Rigidbody2D.velocity = direction.normalized * magnitude;
                _draggableObject.StopDragging();
                ResetTarget();
            }
        }

        private void OnDraggingStarted(Vector2 obj)
        {
            if(_draggableObject != null )
            {
                _draggableObject.StartDragging();
            }
        }

        private void MouseDown(Vector2 point)
        {
            Ray ray = _camera.ScreenPointToRay(_camera.WorldToScreenPoint(point));
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.zero);
            var draggableObject = hit.collider?.GetComponent<DraggableObject>();
            if (draggableObject != null)
            {
                draggableObject.Rigidbody2D.velocity = Vector2.zero;
                SetTarget(draggableObject, point);
            }
        }

        private void Drag(Vector2 point)
        {
            Rigidbody2D rigidbody = _draggableObject?.Rigidbody2D;
            if (rigidbody != null)
            {
                Vector2 targetDirection = GetTargetDirection(point);

                rigidbody.velocity = targetDirection.normalized * Mathf.Min(
                    GetSpeed(rigidbody),
                    targetDirection.magnitude / Time.fixedDeltaTime);

                _pointer.transform.position = point;
            }
        }

        private float GetSpeed(Rigidbody2D rigidbody)
        {
            return _moveSpeed / (rigidbody.mass * _massFactor);
        }

        private Vector2 GetTargetDirection(Vector2 point)
        {
            Vector2 targetPosition = _draggableObject.transform
                                .TransformPoint(_initialOffset);

            Vector2 targetDirection = point - targetPosition;
            return targetDirection;
        }

        private void SetTarget(DraggableObject draggableObject, Vector2 worldHitPoint)
        {
            _draggableObject = draggableObject;
            _pointer.transform.position = worldHitPoint;
            _previousGravityScale = draggableObject.Rigidbody2D.gravityScale;
            _previousCollisionDetectionMode = draggableObject.Rigidbody2D.collisionDetectionMode;
            _initialOffset = draggableObject.transform.InverseTransformPoint(worldHitPoint);

            draggableObject.Rigidbody2D.gravityScale = 0;
            draggableObject.Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            draggableObject.Rigidbody2D.isKinematic = false;
        }

        private void ResetTarget()
        {
            if (_draggableObject != null)
            {
                _draggableObject.Rigidbody2D.gravityScale = _previousGravityScale;
                _draggableObject.Rigidbody2D.collisionDetectionMode = _previousCollisionDetectionMode;
            }
            _draggableObject = null;
        }
    }
}