using System;
using UnityEngine;

namespace DanPie.Framework.UnityExtensions
{
    public static class Vector2Extension
    {
        public static Vector2Int To2Int(this Vector2 v)
        {
            return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
        }

        public static Vector2 Merge(this Vector2 a, Vector2 b, Func<float, float, float> mergeFunc)
        {
            Vector2 result = new Vector2();
            for (int i = 0; i < 2; i++)
            {
                result[i] = mergeFunc(a[i], b[i]);
            }

            return result;
        }
    }
}
