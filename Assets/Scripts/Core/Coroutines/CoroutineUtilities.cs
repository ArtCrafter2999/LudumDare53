using System;
using System.Collections;
using UnityEngine;

namespace DanPie.Framework.Coroutines
{
    public static class CoroutineUtilities
    {
        public static IEnumerator WaitForSeconds(float seconds, Action callback)
        {
            yield return new WaitForSeconds(seconds);
            callback();
        }
    }
}