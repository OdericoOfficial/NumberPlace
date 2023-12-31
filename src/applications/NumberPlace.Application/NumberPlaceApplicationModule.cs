﻿using Godot.Module;
using Godot.Module.DependencyInjection;
using Godot.Module.Serilog;
using Microsoft.Extensions.DependencyInjection;
using NumberPlace.EntityFrameworkCore;

namespace NumberPlace.Application
{
    [DependsOn(typeof(SerilogModule),
        typeof(NumberPlaceEntityFrameworkModule),
        typeof(InjectServiceModule<NumberPlaceApplicationModule>))]
    public class NumberPlaceApplicationModule : CustomModule
    {
        [Serilog(nameof(NumberPlace))]
        public override void ConfigureServices(IServiceCollection services)
        {

        }
    }
}
