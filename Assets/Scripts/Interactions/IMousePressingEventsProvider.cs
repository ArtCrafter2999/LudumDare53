using System;
using UnityEngine;

namespace LudumDare53.Interactions
{
    public interface IMousePressingEventsProvider
    {
        event Action<Vector2> MouseDown;
        event Action<Vector2> MouseUp;
    }
}