using UnityEngine;
using SimpleHeirs;

namespace LudumDare53.Interactions
{
    public class RigidbodyDragger : MonoBehaviour
    {
        [SerializeField] private HeirsProvider<IMouseDragEventsProvider> _mouseDragEventsProvider;
        [SerializeField] private HeirsProvider<IMousePressingEventsProvider> _mousePressingEventsProvider;
        [Min(0)]
        [SerializeField] private float _moveSpeed;
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

            _mousePressingEventsProviderValue.MouseDown += MouseDown;
            _mouseDragEventsProviderValue.Dragged += Drag;
            _mousePressingEventsProviderValue.MouseUp += MouseUp;
        }

        protected void OnDisable()
        {
            _mouseDragEventsProviderValue.DraggingStarted -= MouseDown;
            _mouseDragEventsProviderValue.Dragged -= Drag;
            _mouseDragEventsProviderValue.DraggingStopped -= MouseUp;
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
                Vector2 targetPosition = _draggableObject.transform
                    .TransformPoint(_initialOffset);

                Vector2 targetDirection = point - targetPosition;
                
                rigidbody.velocity = targetDirection.normalized * Mathf.Min(
                    _moveSpeed, 
                    targetDirection.magnitude / Time.fixedDeltaTime);

                _pointer.transform.position = point;
            }
        }

        private void MouseUp(Vector2 point)
        {
            ResetTarget();
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