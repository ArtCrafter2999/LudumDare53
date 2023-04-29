using UnityEngine;

namespace DanPie.Framework.UnityExtensions
{

    public static class TransformExtension
    {
        public static Vector3 ToGlobal(this Transform transform, Vector3 local)
        {
            local.Scale(transform.lossyScale);
            return transform.position + transform.rotation * (local);
        }
    }
}
