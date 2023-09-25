namespace Godot.Module.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TransientAttribute : Attribute
    {
        public virtual Type? ServiceType { get; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TransientAttribute<TService> : TransientAttribute
    {
        private static readonly Type _serviceType = typeof(TService);

        public override Type? ServiceType
            => _serviceType;
    }
}
