namespace Godot.Module.Repository.Abstracts
{
    public interface IEntity<out TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; }
    }
}