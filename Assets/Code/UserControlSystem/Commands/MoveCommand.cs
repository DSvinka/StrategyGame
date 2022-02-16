using Abstractions.Commands;
using UnityEngine;

namespace UserControlSystem.Commands
{
    public sealed class MoveCommand: IMoveCommand
    {
        public Vector3 Target { get; }

        public MoveCommand(Vector3 target)
        {
            Target = target;
        }
    }
}