using System;
using LudumDare53.UI;
using UnityEngine;

namespace LudumDare53.Nodes.Events
{
    [RequireComponent(typeof(UIManager))]
    public class UIChangeByEvent : MonoBehaviour
    {
        public enum Action
        {
            Resume
        }
        [SerializeField] private Action action;
        [SerializeField] private EventNode node;
        private UIManager uiManager;

        private void Start()
        {
            node.invoked.AddListener(Invoke);
        }

        public void Invoke()
        {
            switch (action)
            {
                case Action.Resume:
                    uiManager.Resume();
                    break;
            }
        }
    }
}