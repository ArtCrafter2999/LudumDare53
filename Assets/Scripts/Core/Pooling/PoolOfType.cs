using System;
using System.Collections.Generic;

namespace DanPie.Framework.Pooling
{
    public class PoolOfType<T>
        where T : class, IPoolable
    {
        private string _poolName;
        private Func<T> _typeFactoryMethod;
        private IPoolableObjectBehavior<T> _poolableObjectBehaviorDescriber;
        private List<T> _poolTypeInstances = new List<T>();

        public PoolOfType(
            string poolName,
            Func<T> typeFactoryMethod,
            IPoolableObjectBehavior<T> poolableObjectBehaviorDescriber)
        {
            _poolName = poolName;
            _typeFactoryMethod = typeFactoryMethod;
            _poolableObjectBehaviorDescriber = poolableObjectBehaviorDescriber;
        }

        public string Name { get => _poolName; }
        public Type CollectingObjectsType { get => typeof(T); }
        public int RemainInstancesCount { get => _poolTypeInstances.Count; }
        public IPoolableObjectBehavior<T> PoolableObjectBehaviorDescriber { get => _poolableObjectBehaviorDescriber; }

        public void SetInstancesCount(int instancesCount)
        {
            if (RemainInstancesCount > instancesCount)
            {
                int length = RemainInstancesCount - instancesCount;

                for (int i = 0; i < length; i++)
                {
                    DisposeLastInstance();
                }
            }
            else if (RemainInstancesCount < instancesCount)
            {
                int length = instancesCount - RemainInstancesCount;
                for (int i = 0; i < length; i++)
                {
                    CreateNewInstance();
                }
            }
        }

        public void ReturnObjectInstance(object abstractInstance)
        {
            if (abstractInstance.GetType() != CollectingObjectsType)
            {
                throw new ArgumentException($"Wrong instance type ({abstractInstance.GetType()}), " +
                    $"excepted {CollectingObjectsType}!");
            }
            ReturnInstance((T)(abstractInstance));
        }

        public object TakeObjectInstance() => TakeInstance();

        public void ReturnInstance(T instance)
        {
            _poolableObjectBehaviorDescriber.OnReturnedToPool(instance);
            _poolTypeInstances.Add(instance);
        }

        public T TakeInstance()
        {
            if (RemainInstancesCount == 0)
            {
                CreateNewInstance();
            }

            return PickLastInstance();
        }

        public void DisposeAllInstances()
        {
            SetInstancesCount(0);
        }

        private T PickLastInstance()
        {
            T instance = _poolTypeInstances[RemainInstancesCount - 1];
            _poolTypeInstances.RemoveAt(RemainInstancesCount - 1);
            _poolableObjectBehaviorDescriber.OnTakenFromPool(instance);
            return instance;
        }

        private void CreateNewInstance()
        {
            T instance = _typeFactoryMethod();
            instance.InitPoolableObject(Name);
            _poolableObjectBehaviorDescriber.OnCreated(instance);
            _poolTypeInstances.Add(instance);
        }

        private void DisposeLastInstance()
        {
            T instance = _poolTypeInstances[RemainInstancesCount - 1];
            _poolTypeInstances.RemoveAt(RemainInstancesCount - 1);
            _poolableObjectBehaviorDescriber.OnDisposed(instance);
        }
    }
}
