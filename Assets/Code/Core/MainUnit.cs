using Abstractions;
using Abstractions.Commands;
using UnityEngine;

namespace Core
{
    public sealed class MainUnit : MonoBehaviour, ISelectable, IAttackable
    {
        #region Serialize Fields

        [Header("Information")]
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        
        [Header("Settings")]
        [SerializeField] private float _maxHealth;
        [SerializeField] private Transform _pivotPoint;

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
    }
}