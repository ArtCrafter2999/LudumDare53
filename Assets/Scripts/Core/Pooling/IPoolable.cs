namespace DanPie.Framework.Pooling
{
    public interface IPoolable
    {
        public string PoolName { get; }

        public void InitPoolableObject(string poolName);
    }
}
