using UnityEngine;
using UnityEngine.Events;

namespace LudumDare53.Interactions
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DraggableObject : MonoBehaviour
    {
        public UnityEvent ObjectDraggingStarted;

        private Rigidbody2D _rigidbody2D;

        public Rigidbody2D Rigidbody2D { get => _rigidbody2D; }

        public void StartDragging()
        {
            ObjectDraggingStarted?.Invoke();
        }

        protected void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
}