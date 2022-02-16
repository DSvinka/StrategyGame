using Abstractions.Commands;
using UnityEngine;

namespace UserControlSystem.Commands
{
    public class PatrolCommand: IPatrolCommand
    {
        public Vector3 Target { get; }

        public PatrolCommand(Vector3 target)
        {
            Target = target;
        }
    }
}