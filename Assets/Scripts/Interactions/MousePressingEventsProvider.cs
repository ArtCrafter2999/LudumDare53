using System;
using UnityEngine;

namespace LudumDare53.Interactions
{
    public class MousePressingEventsProvider : MouseEventsProvider, IMousePressingEventsProvider
    {
        private bool _isStartedOnUI;

        public event Action<Vector2> MouseDown;
        public event Action<Vector2> MouseUp;

        protected void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isStartedOnUI = IsPointerHitMaskedLayersUI(Input.mousePosition);
            }

            if (_isStartedOnUI)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                MouseDown?.Invoke(GetWorldPos());
            }
            else if (Input.GetMouseButtonUp(0))
            {
                MouseUp?.Invoke(GetWorldPos());
            }
        }
    }
}
