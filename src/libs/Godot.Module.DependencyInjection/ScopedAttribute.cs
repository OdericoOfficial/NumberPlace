namespace Godot.Module.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ScopedAttribute : Attribute
    {
        public virtual Type? ServiceType { get; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ScopedAttribute<TService> : ScopedAttribute
    {
        private static readonly Type _serviceType = typeof(TService);

        public override Type? ServiceType
            => _serviceType;
    }
}
