using System.Threading.Tasks;
using Abstractions;
using Abstractions.Commands;
using UnityEngine;

namespace Core
{
    public sealed class MainBuilding : MonoBehaviour, ISelectable, IAttackable
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
        
        public Vector3 RallyPoint { get; set; }

        #endregion
        
        private float _health;
        private GameObject _gameObject;

        public void Start()
        {
            _health = _maxHealth;
            _gameObject = gameObject;
        }
        
        public void AddDamage(int amount)
        {
            if (_health <= 0)
            {
                return;
            }
            
            _health -= amount;
            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
