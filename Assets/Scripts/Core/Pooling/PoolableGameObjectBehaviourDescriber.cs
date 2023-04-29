using UnityEngine;

namespace DanPie.Framework.Pooling
{
    public class PoolableGameObjectBehaviourDescriber : IPoolableObjectBehavior<GameObject>
    {
        private string _poolRootName;
        private GameObject _poolRoot;

        public PoolableGameObjectBehaviourDescriber(string poolRootName)
        {
            _poolRootName = poolRootName;
            _poolRoot = GameObject.Find(poolRootName);
            if (_poolRoot == null)
            {
                _poolRoot = new GameObject(_poolRootName);
            }
        }

        public GameObject PoolRoot { get => _poolRoot; }

        public void OnCreated(GameObject poolableObjectInstance)
        {
            OnReturnedToPool(poolableObjectInstance);
        }

        public void OnDisposed(GameObject poolableObjectInstance)
        {
            MonoBehaviour.Destroy(poolableObjectInstance);
        }

        public void OnReturnedToPool(GameObject poolableObjectInstance)
        {
            poolableObjectInstance.SetActive(false);
            poolableObjectInstance.transform.SetParent(_poolRoot.transform);
        }

        public void OnTakenFromPool(GameObject poolableObjectInstance)
        {
            poolableObjectInstance.SetActive(true);
        }
    }
}
