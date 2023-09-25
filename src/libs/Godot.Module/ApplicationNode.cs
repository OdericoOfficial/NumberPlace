using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Godot.Module
{
    public abstract partial class ApplicationNode<TCustomModule> : Node, IServiceProvider 
        where TCustomModule : CustomModule, new()
    {
        private static volatile IServiceProvider? _provider;

        public static IServiceProvider Instance
            => _provider ?? throw new NullReferenceException("ServiceProvider initialize failed");

        public override async void _EnterTree()
        {
            IServiceCollection services = new ServiceCollection();
            IList<CustomModule> modules = new List<CustomModule>();
            HashSet<Type> types = new HashSet<Type>();
            AddAllModule(modules, types, typeof(TCustomModule));

            foreach (var module in modules)
                await module.ConfigureServicesAsync(services);

            IServiceProvider provider = services.BuildServiceProvider();
            foreach (var module in modules)
                await module.OnApplicationInitializationAsync(provider);

            _provider = provider;
        }

        private static void AddAllModule(IList<CustomModule> modules, HashSet<Type> types, Type type)
        {
            types.Add(type);
            var attribute = type.GetCustomAttribute<DependsOnAttribute>();

            if (attribute is not null)
                foreach (var item in attribute.Modules)
                    if (!types.Contains(item))
                        AddAllModule(modules, types, item);

            if (Activator.CreateInstance(type) is CustomModule module)
                modules.Add(module);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object? GetService(Type serviceType)
            => _provider?.GetService(serviceType);
    }
}
