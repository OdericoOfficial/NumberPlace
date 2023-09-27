using Godot;
using Godot.Module;
using Godot.Module.Repository.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NumberPlace.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberPlace.Godot
{
#nullable disable
    public partial class NumberPlaceApplication : ApplicationNode<NumberPlaceModule>
    {
    }
}
