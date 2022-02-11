using UnityEngine;

namespace Abstractions.Commands
{
    public interface IProduceUnitCommand
    {
        GameObject UnitPrefab { get; }
    }
}