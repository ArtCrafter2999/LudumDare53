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

        public Rigidbody2D Rigidbody2D { get => _rigidbody2D; }

        public void StartDragging()
        {
            gameObject.layer = _draggableLayer;
            ObjectDraggingStarted?.Invoke();
        }

        public void StopDragging()
        {
            gameObject.layer = _startLayer;
            ObjectDraggingStopped?.Invoke();
        }

        protected void Start()
        {
            _draggableLayer = LayerMask.NameToLayer(_draggableLayerName);
            _startLayer = gameObject.layer;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
}