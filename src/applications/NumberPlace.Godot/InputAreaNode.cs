using Godot;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberPlace.Godot
{
#nullable disable
    internal partial class InputAreaNode : Area3D
    {
        private ILogger<InputAreaNode> _logger;
        public static event EventHandler<string> OnInputAreaChanged;

        public override void _Ready()
            => _logger = NumberPlaceApplication.Instance.GetRequiredService<ILogger<InputAreaNode>>();

        public override void _InputEvent(Camera3D camera, InputEvent @event, Vector3 position, Vector3 normal, int shapeIdx)
        {
            if (@event is InputEventMouseButton e
                && e.ButtonIndex == MouseButton.Left
                    && e.Pressed)
            {
                _logger.LogInformation(Name);
                OnInputAreaChanged?.Invoke(this, Name);
            }
        }
    }
}
