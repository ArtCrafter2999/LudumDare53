using UnityEngine;
using UnityEngine.Events;

namespace LudumDare53.Interactions
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DraggableObject : MonoBehaviour
    {
        [SerializeField] private string _draggableLayerName = "Draggable";

        public UnityEvent ObjectDraggingStarted;
        public UnityEvent ObjectDraggingStopped;
        private int _draggableLayer;
        private int _startLayer;
        private Rigidbody2D _rigidbody2D;
        private float _startGravityScale;
        private CollisionDetectionMode2D _startCollisionDetectionMode;

        public Rigidbody2D Rigidbody2D { get => _rigidbody2D; }
        public float StartGravityScale { get => _startGravityScale; }
        public CollisionDetectionMode2D StartCollisionDetectionMode { get => _startCollisionDetectionMode; }

        public void OnDraggingStarted()
        {
            gameObject.layer = _draggableLayer;
            ObjectDraggingStarted?.Invoke();
        }

        public void OnDraggingStoped()
        {
            gameObject.layer = _startLayer;
            ObjectDraggingStopped?.Invoke();
        }

        protected void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _startGravityScale = _rigidbody2D.gravityScale;
            _startCollisionDetectionMode = _rigidbody2D.collisionDetectionMode;
            _draggableLayer = LayerMask.NameToLayer(_draggableLayerName);
            _startLayer = gameObject.layer;
        }
    }
}