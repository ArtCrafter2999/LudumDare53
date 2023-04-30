using System;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare53.Core
{

    [Serializable]
    public class ProgressObjectProvider<TValue> 
        where TValue : class
    {
        [SerializeField] private List<ProgressObject<TValue>> _progressObjects = new();

        //public TValue GetObjectByProgress(float progress)
        //{
        //    _progressObjects.Sort((a, b) => a.ProgresMark)
        //}
    }
}
