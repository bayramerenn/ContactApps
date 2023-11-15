namespace Shared.BaseModels
{
    public abstract class BaseEntity<TKey> where TKey : struct
    {
        public TKey Id { get; protected set; }
    }
}