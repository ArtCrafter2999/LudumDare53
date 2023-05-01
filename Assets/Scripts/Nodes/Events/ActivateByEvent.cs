using System;
using LudumDare53.UI;
using UnityEngine;

namespace LudumDare53.Nodes.Events
{
    public class ActivateByEvent : MonoBehaviour
    {
        private enum State
        {
            Active,
            Inactive
        }

        [SerializeField] private EventNode _node;
        [SerializeField] private GameObject target;
        [SerializeField] private State make;
        [SerializeField] private bool invertAtAnd;

        public void Start()
        {
            _node.invoked.AddListener(Invoke);
            _node.broken.AddListener(Break);
        }

        public void Invoke()
        {
            target.SetActive(make == State.Active);
        }

        public void Break()
        {
            if(invertAtAnd) target.SetActive(make != State.Active);
        }
    }
}