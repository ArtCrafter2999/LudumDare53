using UnityEngine;

namespace LudumDare53.Interactions
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DraggableObject : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        public Rigidbody2D Rigidbody2D { get => _rigidbody2D; }

        protected void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
}