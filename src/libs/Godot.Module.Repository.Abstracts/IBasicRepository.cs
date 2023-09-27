namespace Godot.Module.Repository.Abstracts
{
    public interface IBasicRepository<out TEntity, in TKey> where TEntity : class, IEntity<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        IQueryable<TEntity> AsQueryable(bool noTracking = false);
        long Count();
        ValueTask<long> CountAsync();
    }
}
