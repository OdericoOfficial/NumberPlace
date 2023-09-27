namespace Godot.Module.Repository.Abstracts
{
    public interface IRepository<TEntity, TKey> : IBasicRepository<TEntity, TKey>, IReadOnlyRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new() where TKey : IEquatable<TKey>
    {
        void Add(TEntity entity);
        ValueTask AddAsync(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);
        ValueTask AddRangeAsync(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        bool EnsureChanges();
        ValueTask<bool> EnsureChangesAsync();
    }
}
