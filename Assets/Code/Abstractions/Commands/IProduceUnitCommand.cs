using UnityEngine;

namespace Abstractions.Commands
{
    public interface IProduceUnitCommand: IIconHolder
    {
        string UnitName { get; }
        float ProductionTime { get; }

        GameObject UnitPrefab { get; }
    }
}