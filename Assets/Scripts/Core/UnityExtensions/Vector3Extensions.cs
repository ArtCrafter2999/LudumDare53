using System;
using UnityEngine;

namespace DanPie.Framework.UnityExtensions
{
    public static class Vector3Extensions
    {
        public static Vector3 Merge(this Vector3 a, Vector3 b, Func<float, float, float> mergeFunc)
        {
            Vector3 result = new Vector3();
            for (int i = 0; i < 3; i++)
            {
                result[i] = mergeFunc(a[i], b[i]);
            }

            return result;
        }
    }
}
