namespace DanPie.Framework.Pooling
{
    public interface IPoolableObjectBehavior<T>
        where T : class
    {
        public void OnCreated(T poolableObjectInstance);

        public void OnDisposed(T poolableObjectInstance);

        public void OnReturnedToPool(T poolableObjectInstance);

        public void OnTakenFromPool(T poolableObjectInstance);
    }
}
