using UnityEngine;

namespace Abstractions
{
    public interface IUnitProductionTask: IIconHolder
    {
        public string UnitName { get; }
        public float LeftTime { get; }
        public float ProductionTime { get; }
        
        public GameObject UnitPrefab { get; }
    }
}