using Microsoft.Extensions.DependencyInjection;
using NumberPlace.Algorithm.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberPlace.Algorithm
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddMatFactory(this IServiceCollection services)
            => services.AddSingleton<IMatFactory, MatFactory>();
    }
}
