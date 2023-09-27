namespace Godot.Module.Repository.Abstracts
{
    #nullable disable
    public abstract class Entity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }
    }
}
