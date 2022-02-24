using Abstractions;
using Abstractions.Commands;
using UnityEngine;

namespace Core
{
    public sealed class MainBuilding : CommandExecutorBase<IProduceUnitCommand>, ISelectable, IAttackable
    {
        #region Serialize Fields

        [Header("Information")]
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        
        [Header("Settings")]
        [SerializeField] private float _maxHealth;
        [SerializeField] private Transform _pivotPoint;
        
        [Header("Spawning")]
        [SerializeField] private GameObject _unitPrefab;
        [SerializeField] private Transform _unitsParent;

        #endregion

        #region Public Properties 

        public float Health => _health;
        public float MaxHealth => _maxHealth;

        public string Name => _name;
        public Sprite Icon => _icon;
        
        public GameObject GameObject => _gameObject;
        public Transform PivotPoint => _pivotPoint;

        #endregion
        
        private float _health;
        private GameObject _gameObject;

        public void Start()
        {
            _health = _maxHealth;
            _gameObject = gameObject;
        }

        public override void ExecuteSpecific(IProduceUnitCommand command)
        {
            Instantiate(
                command.UnitPrefab, 
                new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), 
                Quaternion.identity, _unitsParent);
        }
    }
}
