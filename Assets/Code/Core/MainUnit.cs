using Abstractions;
using Abstractions.Commands;
using Commands;
using Core.CommandExecutors;
using UnityEngine;

namespace Core
{
    public sealed class MainUnit : MonoBehaviour, ISelectable, IAttackable, IDamageDealer
    {
        private static readonly int Dead = Animator.StringToHash("Dead");
        
        #region Serialize Fields

        [Header("Information")]
        [SerializeField] private string _name = "Unit";
        [SerializeField] private Sprite _icon;
        
        [Header("Settings")]
        [SerializeField] private int _damage = 25;
        [SerializeField] private float _maxHealth = 100;
        [SerializeField] private Transform _pivotPoint;
        
        [Header("Компоненты")]
        [SerializeField] private Animator _animator;
        [SerializeField] private StopCommandExecutor _stopCommand;
        
        #endregion

        #region Public Properties 

        public int Damage => _damage;
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
        
        public void AddDamage(int amount)
        {
            if (_health <= 0)
            {
                return;
            }
            _health -= amount;
            if (_health <= 0)
            {
                _animator.SetTrigger(Dead);
                Invoke(nameof(Destroy), 1f);
            }
        }
        
        private async void Destroy()
        {
            await _stopCommand.ExecuteSpecific(new StopCommand());
            Destroy(gameObject);
        }
    }
}