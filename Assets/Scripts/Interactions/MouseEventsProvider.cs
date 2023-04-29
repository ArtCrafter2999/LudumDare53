using System.Linq;
using DanPie.Framework.UnityExtensions;
using UnityEngine;

namespace LudumDare53.Interactions
{
    public abstract class MouseEventsProvider : MonoBehaviour
    {
        [SerializeField] private IgnoredLayers _ignoredUILayers;

        protected Camera Camera;
        protected Vector2 DragStartPos;
        protected Vector2Int ScreenSize;
        protected int[] IgnoredUILayers;

        protected virtual void Start()
        {
            ScreenSize = new Vector2Int(Screen.width, Screen.height);
            Camera = Camera.main;
            IgnoredUILayers = _ignoredUILayers.GetIgnoredLayers();
        }

        protected float GetDistance()
        {
            Vector2 relativePos = GetRelativeMousePosition();
            float distance = Vector2.Distance(DragStartPos, relativePos);
            return distance;
        }

        protected Vector2 GetRelativeMousePosition()
        {
            Vector2 clickPos = Input.mousePosition;
            return new Vector2(clickPos.x / ScreenSize.x, clickPos.y / ScreenSize.y);
        }

        protected Vector2 GetWorldPos()
            => Camera.GetWorldPosOnPlane(Input.mousePosition);

        protected bool IsPointerHitMaskedLayersUI(Vector3 pointer)
        {
            return Camera.IsPointerHitUI(pointer, IgnoredUILayers);
        }
    }
}