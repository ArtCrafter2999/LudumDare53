using UnityEngine;
using SimpleHeirs;
using LudumDare53.Leveling;
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
        private Vector2 _initialOffset;
        private DraggableObject _draggableObject;
        private bool _isPaused;

        public bool IsDragging { get => _draggableObject != null; }

        public void StopDragging()
        {
            MouseUp(Vector2.zero);
        }

        protected void OnEnable()
        {
            _isPaused = PauseManager.IsPaused;
            ResetTarget();
            _camera = Camera.main;
            _mouseDragEventsProviderValue = _mouseDragEventsProvider.GetValue();
            _mousePressingEventsProviderValue = _mousePressingEventsProvider.GetValue();

            _mouseDragEventsProviderValue.Dragged += Drag;
            _mouseDragEventsProviderValue.DraggingStarted += OnDraggingStarted;
            _mousePressingEventsProviderValue.MouseDown += MouseDown;
            _mousePressingEventsProviderValue.MouseUp += MouseUp;
            PauseManager.Pause.AddListener(OnPause);
            PauseManager.Resume.AddListener(OnResume);
        }

        
        protected void OnDisable()
        {
            _mouseDragEventsProviderValue.Dragged -= Drag;
            _mouseDragEventsProviderValue.DraggingStarted -= OnDraggingStarted;
            _mousePressingEventsProviderValue.MouseDown -= MouseDown;
            _mousePressingEventsProviderValue.MouseUp -= MouseUp;
            PauseManager.Pause.RemoveListener(OnPause);
            PauseManager.Resume.RemoveListener(OnResume);
        }

        private void OnPause()
        {
            _isPaused = true;
            ResetTarget();
        }

        private void OnResume()
        {
            _isPaused = false;
        }


        private void MouseUp(Vector2 point)
        {
            if (IsDragging)
            {
                _draggableObject.OnDraggingStoped();
                ResetTarget();
            }
        }

        private void OnDraggingStarted(Vector2 obj)
        {
            if(IsDragging)
            {
                _draggableObject.OnDraggingStarted();
            }
        }

        private void MouseDown(Vector2 point)
        {

            if(_isPaused)
            {
                return;
            }

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

                rigidbody.velocity += targetDirection.normalized * GetSpeed(rigidbody);

                float maxMagnitude 
                    = Mathf.Min(targetDirection.magnitude / Time.fixedDeltaTime, _moveSpeed);
                float currentMagnitude = Mathf.Min(rigidbody.velocity.magnitude, maxMagnitude);
                rigidbody.velocity = targetDirection.normalized * currentMagnitude;
                _pointer.transform.position = point;
            }
        }

        private float GetSpeed(Rigidbody2D rigidbody)
        {
            return (_moveSpeed / (rigidbody.mass * _massFactor)) * Time.deltaTime;
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
            _initialOffset = draggableObject.transform.InverseTransformPoint(worldHitPoint);

            draggableObject.Rigidbody2D.gravityScale = 0;
            draggableObject.Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            draggableObject.Rigidbody2D.isKinematic = false;
        }

        private void ResetTarget()
        {
            if (_draggableObject != null)
            {
                _draggableObject.Rigidbody2D.gravityScale = _draggableObject.StartGravityScale;
                _draggableObject.Rigidbody2D.collisionDetectionMode = _draggableObject.StartCollisionDetectionMode;
            }
            _draggableObject = null;
        }
    }
}