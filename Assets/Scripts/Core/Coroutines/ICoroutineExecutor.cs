using System.Collections;
using UnityEngine;

namespace DanPie.Framework.Coroutines
{
    public interface ICoroutineExecutor
    {
        void BreakCoroutine(Coroutine coroutine);
        Coroutine ExecuteCoroutine(IEnumerator coroutine);
    }
}