using System.Collections;
using UnityEngine;

namespace DanPie.Framework.Coroutines
{
    public class CoroutineExecutor : MonoBehaviour, ICoroutineExecutor
    {
        public Coroutine ExecuteCoroutine(IEnumerator coroutine)
        {
            return StartCoroutine(coroutine);
        }

        public void BreakCoroutine(Coroutine coroutine)
        {
            StopCoroutine(coroutine);
        }
    }
}
