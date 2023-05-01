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
            FadeOut,
            Resume
        }
        [SerializeField] private Action action;
        [SerializeField] private EventNode node;
        private UIManager uiManager;

        private void Start()
        {
            uiManager = GetComponent<UIManager>();
            node.initialized.AddListener(Invoke);
        }

        public void Invoke()
        {
            switch (action)
            {
                case Action.Resume:
                    uiManager.Resume();
                    break;
                case Action.FadeOut:
                    uiManager.SmoothFadeOut();
                    break;
            }
        }
        
    }
}