using UnityEngine;
using SimpleHeirs;
using static UnityEngine.GridBrushBase;

namespace LudumDare53.Interactions
{

    public class RigidbodyDragger : MonoBehaviour
    {
        [SerializeField] private HeirsProvider<IMouseDragEventsProvider> _mouseDragEventsProvider;
        [SerializeField] private HeirsProvider<IMousePressingEventsProvider> _mousePressingEventsProvider;
        [Min(0)]
        [SerializeField] private Vector2 _minMaxMoveSpeed = new Vector2(2, 10);
        [SerializeField] private float _lookAtGroundForce = 3f;
        [SerializeField] private Rigidbody2D _pointer;

        private Camera _camera;
        private IMouseDragEventsProvider _mouseDragEventsProviderValue;
        private IMousePressingEventsProvider _mousePressingEventsProviderValue;
        private float _previousGravityScale;
        private CollisionDetectionMode2D _previousCollisionDetectionMode;
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
                SetTarget(draggableObject, point);
            }
        }

        private void Drag(Vector2 point)
        {
            Rigidbody2D rigidbody = _draggableObject?.Rigidbody2D;
            if (rigidbody != null)
            {
                Vector2 targetPosition = _draggableObject.transform
                    .TransformPoint(_draggableObject.SpringJoint2D.anchor);

                Vector2 targetDirection = point - targetPosition;

                float maxMag 
                    = Mathf.Min(_minMaxMoveSpeed.y, targetDirection.magnitude / Time.fixedDeltaTime);

                float minMag = _minMaxMoveSpeed.x;

                float magnitude = rigidbody.velocity.magnitude;
                if (maxMag <= minMag)
                {
                    magnitude = Mathf.Min(maxMag, magnitude);  
                }
                else
                {
                    magnitude = Mathf.Clamp(magnitude, minMag, maxMag);  
                }
                rigidbody.velocity = targetDirection.normalized * magnitude;
                //AddTorque(_draggableObject, targetPosition);
                _pointer.transform.position = point;
            }
        }

        private void MouseUp(Vector2 point)
        {
            ResetTarget();
        }

        private void AddTorque(DraggableObject draggableObject, Vector2 targetPosition)
        {
            Vector2 pointToRotateAround = targetPosition;
            Vector2 pointOnObject = transform.position;

            Vector2 spriteDirection = (draggableObject.transform.TransformPoint(
                -draggableObject.SpringJoint2D.anchor) - draggableObject.transform.position).normalized;

            //var anchor = draggableObject.SpringJoint2D.anchor;
            //Debug.DrawRay(
            //    draggableObject.transform.TransformPoint(new Vector3(anchor.x, anchor.y)),
            //    spriteDirection, Color.red, 2f);
            //float sign = Vector2.Dot(spriteDirection, Physics.gravity) < 0 ? -1f : 1f;
            float sign = Vector2.Dot(spriteDirection, Physics.gravity) < 0 ? -1f : 1f;
            float angleFactor = Vector2.Angle(spriteDirection, Physics.gravity);
            Debug.Log(sign);
            float rotationTorque = _lookAtGroundForce * sign * Time.fixedDeltaTime;
            draggableObject.Rigidbody2D.AddTorque(rotationTorque);
        }

        private void SetTarget(DraggableObject draggableObject, Vector2 worldHitPoint)
        {
            _draggableObject = draggableObject;
            _pointer.transform.position = worldHitPoint;
            _previousGravityScale = draggableObject.Rigidbody2D.gravityScale;
            _previousCollisionDetectionMode = draggableObject.Rigidbody2D.collisionDetectionMode;

            draggableObject.SpringJoint2D.enabled = true;
            draggableObject.SpringJoint2D.connectedBody = _pointer;
            draggableObject.SpringJoint2D.anchor = draggableObject.transform
                .InverseTransformPoint(worldHitPoint);

            draggableObject.Rigidbody2D.gravityScale = 0;
            draggableObject.Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        private void ResetTarget()
        {
            if (_draggableObject != null)
            {
                _draggableObject.SpringJoint2D.enabled = false;
                _draggableObject.SpringJoint2D.connectedBody = null;
                _draggableObject.Rigidbody2D.gravityScale = _previousGravityScale;
                _draggableObject.Rigidbody2D.collisionDetectionMode = _previousCollisionDetectionMode;
            }
            _draggableObject = null;
        }
    }
}