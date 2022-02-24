using UnityEngine;

namespace Abstractions.Commands
{
    public interface IPatrolCommand
    {
        public Vector3 From { get; }
        public Vector3 To { get; }
    }
}