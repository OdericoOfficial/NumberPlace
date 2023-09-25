using Godot.Module;
using Godot.Module.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NumberPlace.EntityFrameworkCore
{
    [DependsOn(typeof(EntityFrameworkCoreModule<NumberPlaceDbContext, SqliteActionWrapper>))]
    internal class NumberPlaceEntityFrameworkModule : CustomModule
    {

    }
}
