using System;
using UnityEngine;
using System.Linq;

namespace LudumDare53.Interactions
{
    public interface IMouseDragEventsProvider
    {
        Vector2 LastActionScreenPoint { get; }

        event Action<Vector2> DraggingStarted;
        event Action<Vector2> Dragged;
        event Action<Vector2> DraggingStopped;
    }
}