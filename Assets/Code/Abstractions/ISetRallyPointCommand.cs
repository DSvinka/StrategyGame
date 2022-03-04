using UnityEngine;

namespace Abstractions
{
    public interface ISetRallyPointCommand
    {
        Vector3 RallyPoint { get; }
    }
}