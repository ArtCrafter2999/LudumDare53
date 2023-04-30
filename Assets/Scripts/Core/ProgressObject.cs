using System;
using UnityEngine;

namespace LudumDare53.Core
{
    [Serializable]
    public class ProgressObject<TValue>
        where TValue : class
    {
        [SerializeField] private float _progresMark;
        [SerializeField] private TValue _objectValue;

        public float ProgresMark { get => _progresMark; }
        public TValue ObjectValue { get => _objectValue; }

        public ProgressObject(float progresMark, TValue objectValue)
        {
            _progresMark = progresMark;
            _objectValue = objectValue;
        }
    }
}
