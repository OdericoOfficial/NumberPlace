using Godot.Module;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Godot.Module.DependencyInjection
{
    public class InjectServiceModule<T> : CustomModule
    {
        public override void ConfigureServices(IServiceCollection services)
            => services.AddAllServices(typeof(T).Assembly);
    }
}
