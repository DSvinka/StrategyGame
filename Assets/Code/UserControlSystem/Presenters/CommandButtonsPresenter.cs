using System;
using System.Collections.Generic;
using Utils;
using Abstractions;
using Abstractions.Commands;
using UserControlSystem.Views;
using UserControlSystem.Models;
using UserControlSystem.Models.Commands;
using UnityEngine;

namespace UserControlSystem.Presenters
{
    public sealed class CommandButtonsPresenter: MonoBehaviour
    {
        [SerializeField] private SelectableValue _selectable;
        [SerializeField] private CommandButtonsView _view;
        [SerializeField] private AssetsContext _context;

        private ISelectable _currentSelectable;

        private void Start()
        {
            _selectable.OnSelected += OnSelected;
            OnSelected(_selectable.CurrentValue);

            _view.OnClick += OnButtonClick;
        }
        
        private void OnSelected(ISelectable selectable)
        {
            if (_currentSelectable == selectable)
                return;

            _currentSelectable = selectable;
            _view.Clear();
            
            if (selectable != null)
            {
                var commandExecutors = new List<ICommandExecutor>();
                commandExecutors.AddRange(selectable.GameObject.GetComponentsInParent<ICommandExecutor>());
                _view.MakeLayout(commandExecutors);
            }
        }
        
        private void OnButtonClick(ICommandExecutor commandExecutor)
        {
            var unitProducerCommand = commandExecutor as CommandExecutorBase<IProduceUnitCommand>;
            if (unitProducerCommand != null)
            {
                unitProducerCommand.Execute(_context.Inject(new ProduceUnitCommand()));
                return;
            }
            
            var attackCommand = commandExecutor as CommandExecutorBase<IAttackCommand>;
            if (attackCommand != null)
            {
                attackCommand.Execute(new AttackCommand());
                return;
            }
            
            var moveCommand = commandExecutor as CommandExecutorBase<IMoveCommand>;
            if (moveCommand != null)
            {
                moveCommand.Execute(new MoveCommand());
                return;
            }
            
            var patrolCommand = commandExecutor as CommandExecutorBase<IPatrolCommand>;
            if (patrolCommand != null)
            {
                patrolCommand.Execute(new PatrolCommand());
                return;
            }
            
            var stopCommand = commandExecutor as CommandExecutorBase<IStopCommand>;
            if (stopCommand != null)
            {
                stopCommand.Execute(new StopCommand());
                return;
            }

            throw new ApplicationException(
                $"{nameof(CommandButtonsPresenter)}.{nameof(OnButtonClick)}: Unknown type of commands executor: {commandExecutor.GetType().FullName}");
        }
    }
}