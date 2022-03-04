using Abstractions;
using UnityEngine;

namespace Core
{
    public sealed class UnitProductionTask: IUnitProductionTask
    {
        public string UnitName { get; }
        public float LeftTime { get; set; }
        public float ProductionTime { get; }
        public Sprite Icon { get; }
        public GameObject UnitPrefab { get; }

        public UnitProductionTask(float time, string unitName, Sprite icon, GameObject unitPrefab)
        {
            ProductionTime = time;
            LeftTime = time;

            UnitName = unitName;
            Icon = icon;
            UnitPrefab = unitPrefab;
        }
    }
}