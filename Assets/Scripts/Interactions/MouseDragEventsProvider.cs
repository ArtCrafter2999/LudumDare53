using System;
using UnityEngine;

namespace LudumDare53.Interactions
{
    public class MouseDragEventsProvider : MouseEventsProvider, IMouseDragEventsProvider
    {
        [SerializeField] private float _deadZone = 0.1f;

        private Vector2 _lastDragPos;
        private bool _isDragging = false;
        private Vector2 _lastActionScreenPoint;
        private bool _isStartedOnUI;

        public event Action<Vector2> DraggingStarted;
        public event Action<Vector2> Dragged;
        public event Action<Vector2> DraggingStopped;

        public Vector2 LastActionScreenPoint { get => _lastActionScreenPoint; }

        protected void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isStartedOnUI = IsPointerHitMaskedLayersUI(Input.mousePosition);
            }

            if (!_isStartedOnUI)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    DragStartPos = GetRelativeMousePosition();
                    _lastDragPos = GetWorldPos();
                }
                else if (Input.GetMouseButton(0) && (GetDistance() > _deadZone || _isDragging))
                {
                    if (!_isDragging)
                    {
                        _isDragging = true;
                        _lastDragPos = GetWorldPos();
                        _lastActionScreenPoint = Input.mousePosition;
                        DraggingStarted?.Invoke(_lastDragPos);
                    }

                    Vector2 dragPos = GetWorldPos();
                    Vector2 delta = (_lastDragPos - dragPos);
                    _lastActionScreenPoint = Input.mousePosition;
                    Dragged?.Invoke(dragPos);
                    _lastDragPos = GetWorldPos();
                }
                else if (Input.GetMouseButtonUp(0) && !_isDragging)
                {
                    _lastActionScreenPoint = Input.mousePosition;
                    DraggingStopped?.Invoke(GetWorldPos());
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    _isDragging = false;
                }
            }
        }
    }
}
