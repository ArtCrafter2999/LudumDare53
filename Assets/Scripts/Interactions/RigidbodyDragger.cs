using UnityEngine;
using SimpleHeirs;

namespace LudumDare53.Interactions
{
    public class RigidbodyDragger : MonoBehaviour
    {
        [SerializeField] private HeirsProvider<IMouseDragEventsProvider> _mouseDragEventsProvider;
        [SerializeField] private HeirsProvider<IMousePressingEventsProvider> _mousePressingEventsProvider;
        [Min(0)]
        [SerializeField] private float _dragForce = 2f;
        [SerializeField] private Transform _pointer;

        private Camera _camera;
        private IMouseDragEventsProvider _mouseDragEventsProviderValue;
        private IMousePressingEventsProvider _mousePressingEventsProviderValue;
        private RaycastHit2D _hit;
        private float _previousGravityScale;
        private CollisionDetectionMode2D _previousCollisionDetectionMode;

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
            if (hit.rigidbody != null)
            {
                SetTarget(hit);
                hit.rigidbody.velocity = Vector2.zero;
            }
        }

        private void Drag(Vector2 point)
        {
            Rigidbody2D rigidbody = _hit.rigidbody;
            if (rigidbody != null )
            {
                _pointer.transform.position = point;
                Vector2 targetPos = rigidbody.transform.position;
                Vector2 moveDirection = (point - targetPos) * Time.fixedDeltaTime * _dragForce;
                rigidbody.velocity = moveDirection;
            }
        }

        private void MouseUp(Vector2 point)
        {
            ResetTarget();
        }

        private void SetTarget(RaycastHit2D hit)
        {
            _hit = hit;
            Rigidbody2D rigidbody = _hit.rigidbody;
            _previousGravityScale = rigidbody.gravityScale;
            _previousCollisionDetectionMode = rigidbody.collisionDetectionMode;
            rigidbody.gravityScale = 0;
            rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        private void ResetTarget()
        {
            Rigidbody2D rigidbody = _hit.rigidbody;
            if (rigidbody != null)
            {
                rigidbody.gravityScale = _previousGravityScale;
                rigidbody.collisionDetectionMode = _previousCollisionDetectionMode;
            }
            _hit = new RaycastHit2D();
        }
    }
}