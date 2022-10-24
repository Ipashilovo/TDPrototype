namespace Core.GameplaySystems
{
    public interface IPool<T>
    {
        public T Get();
        public void Add(T obj);
    }
}