using UnityEngine;

namespace DanPie.Framework.Pooling
{
    public class GameObjectBehaviourDescriberToComponentAdapter<T> : IPoolableObjectBehavior<T>
        where T: Component
    {
        private string _poolRootName;
        private PoolableGameObjectBehaviourDescriber _poolableGameObjectBehaviourDescriber;

        public GameObjectBehaviourDescriberToComponentAdapter(string poolRootName)
        {
            _poolRootName = poolRootName;
            _poolableGameObjectBehaviourDescriber = new PoolableGameObjectBehaviourDescriber(_poolRootName); 
        }

        public GameObject PoolRoot { get => _poolableGameObjectBehaviourDescriber.PoolRoot; }

        public void OnCreated(T poolableObjectInstance)
        {
            _poolableGameObjectBehaviourDescriber.OnCreated(poolableObjectInstance.gameObject);
        }

        public void OnDisposed(T poolableObjectInstance)
        {
            if (poolableObjectInstance != null)
            {
                _poolableGameObjectBehaviourDescriber.OnDisposed(poolableObjectInstance.gameObject);
            }
        }

        public void OnReturnedToPool(T poolableObjectInstance)
        {
            if (poolableObjectInstance != null)
            {
                _poolableGameObjectBehaviourDescriber.OnReturnedToPool(poolableObjectInstance.gameObject);
            }
        }

        public void OnTakenFromPool(T poolableObjectInstance)
        {
            _poolableGameObjectBehaviourDescriber.OnTakenFromPool(poolableObjectInstance.gameObject);
        }
    }
}
