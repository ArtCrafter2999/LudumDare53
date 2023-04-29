using System;
using System.Collections.Generic;
using System.Linq;

namespace DanPie.Framework.Pooling
{
    public class PoolsHub<T>
        where T : class, IPoolable
    {
        private List<PoolOfType<T>> _pools = new List<PoolOfType<T>>();

        public void DisposeAllPools()
        {
            foreach (PoolOfType<T> pool in _pools)
            {
                pool.DisposeAllInstances();
            }
            _pools.Clear();
        }

        public bool IsPoolExist(string poolName)
        {
            return _pools.Any((x) => x.Name == poolName);
        }

        public void ReturnInstanceToPool(T instance)
        {
            GetPoolByName(instance.PoolName).ReturnInstance(instance);
        }

        public T GetInstanceFromPool(string poolName)
        {
            return GetPoolByName(poolName).TakeInstance();
        }

        public void CreateNewPool(
            string name,
            Func<T> typeFactoryMethod,
            IPoolableObjectBehavior<T> poolableObjectBehaviorDescriber,
            int initialInstancesCount)
        {
            if (IsPoolExist(name))
            {
                throw new ArgumentException($"The pool with poolName: ({name}) is already exist!");
            }

            PoolOfType<T> pool = new PoolOfType<T>(name, typeFactoryMethod, poolableObjectBehaviorDescriber);
            _pools.Add(pool);
            pool.SetInstancesCount(initialInstancesCount);
        }

        private PoolOfType<T> GetPoolByName(string name)
        {
            foreach (PoolOfType<T> pool in _pools)
            {
                if (pool.Name == name)
                {
                    return pool;
                }
            }

            throw new Exception($"No pools named: {name}!");
        }
    }
}
