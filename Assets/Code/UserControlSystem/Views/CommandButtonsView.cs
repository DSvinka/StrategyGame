using System;
using System.Collections.Generic;
using System.Linq;
using Abstractions;
using Abstractions.Commands;
using UnityEngine;
using UnityEngine.UI;

namespace UserControlSystem.Views
{
    public sealed class CommandButtonsView: MonoBehaviour
    {
        public event Action<ICommandExecutor> OnClick;
        
        [SerializeField] private Button _moveButton;
        [SerializeField] private Button _patrolButton;
        [SerializeField] private Button _stopButton;
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _produceUnitButton;

        private Dictionary<Type, Button> _buttonsByExecutorType;

        private void Start()
        {
            _buttonsByExecutorType = new Dictionary<Type, Button>();
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IMoveCommand>), _moveButton);
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IPatrolCommand>), _patrolButton);
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IStopCommand>), _stopButton);
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IAttackCommand>), _attackButton);
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IProduceUnitCommand>), _produceUnitButton);
        }

        private void OnDestroy()
        {
            foreach (var button in _buttonsByExecutorType)
            {
                button.Value.onClick.RemoveAllListeners();
            }
        }

        public void MakeLayout(IEnumerable<ICommandExecutor> commandExecutors)
        {
            foreach (var commandExecutor in commandExecutors)
            {
                var button = GetButton(commandExecutor);
                button.gameObject.SetActive(true);
                button.onClick.AddListener(() => OnClick?.Invoke(commandExecutor));
            }
        }
        
        public void Clear()
        {
            foreach (var button in _buttonsByExecutorType)
            {
                button.Value.onClick.RemoveAllListeners();
                button.Value.gameObject.SetActive(false);
            }
        }

        private Button GetButton(object executorType)
        {
            return _buttonsByExecutorType
                .First(type => type.Key.IsInstanceOfType(executorType))
                .Value;
        }

        private void SetInteractable(bool value)
        {
            foreach (var button in _buttonsByExecutorType)
            {
                button.Value.interactable = value;
            }
        }

        public void BlockInteractions(ICommandExecutor commandExecutor)
        {
            UnblockAllInteractions();
            GetButton(commandExecutor).interactable = false;
        }

        public void BlockAllInteractions() => SetInteractable(false);
        public void UnblockAllInteractions() => SetInteractable(true);
    }
}