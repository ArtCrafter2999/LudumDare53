using UnityEngine;

namespace LudumDare53.Interactions
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpringJoint2D))]
    public class DraggableObject : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private SpringJoint2D _springJoint2D;

        public Rigidbody2D Rigidbody2D { get => _rigidbody2D; }
        public SpringJoint2D SpringJoint2D { get => _springJoint2D; }

        protected void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _springJoint2D = GetComponent<SpringJoint2D>();
            _springJoint2D.enabled = false;
        }
    }
}