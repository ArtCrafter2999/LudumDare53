using System;
using LudumDare53.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace LudumDare53.Nodes.Events
{
    public class TextActivateByEvent : MonoBehaviour
    {
        private enum State
        {
            Active,
            Inactive
        }

        [SerializeField] private EventNode node;
        [SerializeField] private GameObject target;
        [SerializeField] private TextAppearance textAppearance;
        [SerializeField] private State make;
        [SerializeField] private bool invertAtAnd;

        public void Start()
        {
            node.initialized.AddListener(Invoke);
            node.invoked.AddListener(() => node.canSkip = textAppearance.isEnd);
            node.skipped.AddListener(Skip);
            node.broken.AddListener(Break);
        }

        private void Invoke()
        {
            target.SetActive(make == State.Active);
            textAppearance.Activate();
        }

        private void Skip()
        {
            textAppearance.ForceEnd();
            node.canSkip = true;
        }

        private void Break()
        {
            textAppearance.ForceEnd();
            if(invertAtAnd) target.SetActive(make != State.Active);
        }
    }
}