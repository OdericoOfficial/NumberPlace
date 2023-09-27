using Godot.Module;
using NumberPlace.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberPlace.Godot
{
    [DependsOn(typeof(NumberPlaceApplicationModule))]
    public class NumberPlaceModule : CustomModule
    {
    }
}
