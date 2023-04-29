using System;

namespace DanPie.Framework.Pooling
{
    public class PoolObjectInitializingBehaviour<T> : IPoolableObjectBehavior<T>
        where T : class
    {
        public Action<T> OnCreatedAction { get; set; }
        public Action<T> OnDisposedAction { get; set; }
        public Action<T> OnReturnedToPoolAction { get; set; }
        public Action<T> OnTakenFromPoolAction { get; set; }

        public void OnCreated(T poolableObjectInstance)
        {
            OnCreatedAction?.Invoke(poolableObjectInstance);
        }

        public void OnDisposed(T poolableObjectInstance)
        {
            OnDisposedAction?.Invoke(poolableObjectInstance);
        }

        public void OnReturnedToPool(T poolableObjectInstance)
        {
            OnReturnedToPoolAction?.Invoke(poolableObjectInstance);
        }

        public void OnTakenFromPool(T poolableObjectInstance)
        {
            OnTakenFromPoolAction?.Invoke(poolableObjectInstance);
        }
    }
}
