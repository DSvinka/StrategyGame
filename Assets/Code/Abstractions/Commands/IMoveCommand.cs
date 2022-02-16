using UnityEngine;

namespace Abstractions.Commands
{
    public interface IMoveCommand
    {
        public Vector3 Target { get; }
    }
}