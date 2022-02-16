using Abstractions.Commands;
using UnityEngine;
using Utils;

namespace UserControlSystem.Commands
{
    public sealed class ProduceUnitCommand : IProduceUnitCommand
    {
        [InjectAsset("Chomper")] private GameObject _unitPrefab;
        public GameObject UnitPrefab => _unitPrefab;
    }
}