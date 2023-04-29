using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DanPie.Framework.UnityExtensions
{
    public static class CameraExtension
    {
        public static bool IsPointerHitUI(
            this Camera camera,
            Vector3 pointerScreenPosition,
            int[] layerMasks = null)
        {   
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = pointerScreenPosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            foreach (RaycastResult result in results)
            {
                if (layerMasks == null || !layerMasks.Contains(result.gameObject.layer))
                {
                    return true;
                }
            }

            return false;
        }

        public static Vector3 GetWorldPosOnPlane(
           this Camera camera,
           Vector3 screenPoint,
           float distanceToPlane = 10)
        {
            var plane = new Plane(
                -camera.transform.forward,
                camera.transform.forward * distanceToPlane);

            Vector3 hitPoint = Vector3.zero;
            Ray ray = camera.ScreenPointToRay(screenPoint);

            if (plane.Raycast(ray, out float distance))
            {
                hitPoint = ray.GetPoint(distance);
            }

            return hitPoint;
        }

        public static float GetRadiusOfSphereFittedInCamera(this Camera camera)
        {
            float radAngle = camera.fieldOfView * Mathf.Deg2Rad;
            float radHFOV = (float)(2 * Math.Atan(Mathf.Tan(radAngle / 2) * camera.aspect));
            float hFOV = Mathf.Rad2Deg * radHFOV;
            float horizontalFOVRadius = CalculateRadius(camera.farClipPlane, hFOV);
            float verticalFOVRadius = CalculateRadius(camera.farClipPlane, camera.fieldOfView);
            float r = Mathf.Min(horizontalFOVRadius, verticalFOVRadius);
            return r;
        }

        private static float CalculateRadius(float farClipPlane, float angle)
        {
            float g = farClipPlane / (Mathf.Cos(angle / 2f / Mathf.Rad2Deg));
            float k = (Mathf.Tan(angle / 2f / Mathf.Rad2Deg)) * farClipPlane * 2;
            float p = (g * 2 + k) / 2f;
            float s = Mathf.Sqrt(p * (p - g) * (p - g) * (p - k));
            return s / p;
        }
    }
}
