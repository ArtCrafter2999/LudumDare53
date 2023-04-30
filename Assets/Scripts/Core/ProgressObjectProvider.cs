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

        public TValue GetObjectByProgress(float progress)
        {
            if (_progressObjects.Count == 0)
            {
                return null;
            }

            float maxProgress = _progressObjects[0].ProgresMark;
            TValue maxValue = _progressObjects[0].ObjectValue;
            foreach (var progressObject in _progressObjects)
            {
                if (progressObject.ProgresMark <= progress && 
                    progressObject.ProgresMark > maxProgress)
                {
                    maxProgress = progressObject.ProgresMark;
                    maxValue = progressObject.ObjectValue;
                }
            }

            return maxValue;
        }
    }
}
