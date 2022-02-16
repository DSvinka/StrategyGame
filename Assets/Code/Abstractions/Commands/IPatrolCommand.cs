using UnityEngine;

namespace Abstractions.Commands
{
    public interface IPatrolCommand
    {
        public Vector3 Target { get; }
    }
}