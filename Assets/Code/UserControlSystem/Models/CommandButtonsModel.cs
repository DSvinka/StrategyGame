using System;
using System.Runtime.InteropServices;
using Abstractions;
using Abstractions.Commands;
using UnityEngine;
using UserControlSystem.Models.CommandCreators;
using Zenject;

namespace UserControlSystem.Models
{
    public sealed class CommandButtonsModel
    {
        public event Action<ICommandExecutor> OnCommandAccepted;
        public event Action OnCommandComplete;
        public event Action OnCommandCancel;

        [Inject] private CommandCreatorBase<IProduceUnitCommand> _unitProducer;
        [Inject] private CommandCreatorBase<IAttackCommand> _attacker;
        [Inject] private CommandCreatorBase<IMoveCommand> _moving;
        [Inject] private CommandCreatorBase<IPatrolCommand> _patrolling;
        [Inject] private CommandCreatorBase<IStopCommand> _stopped;
        [Inject] private CommandCreatorBase<IStopCommand> _setRally;

        private bool _commandIsPending;

        public void OnCommandButtonClicked(ICommandExecutor commandExecutor, ICommandsQueue commandsQueue)
        {
            if (_commandIsPending)
            {
                OnProcessCancel();
            }
            _commandIsPending = true;
            OnCommandAccepted?.Invoke(commandExecutor);

            _unitProducer.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
            _attacker.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
            _stopped.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
            _moving.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
            _patrolling.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
            _setRally.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
        }

        public void ExecuteCommandWrapper(object command, ICommandsQueue commandsQueue)
        {
            if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
            {
                commandsQueue.Clear();
            }
            commandsQueue.EnqueueCommand(command);
            _commandIsPending = false;
            OnCommandComplete?.Invoke();
        }

        public void OnSelectionChanged()
        {
            _commandIsPending = false;
            OnProcessCancel();
        }

        public void OnProcessCancel()
        {
            _unitProducer.ProcessCancel();
            _setRally.ProcessCancel();
            
            _attacker.ProcessCancel();
            _moving.ProcessCancel();
            _patrolling.ProcessCancel();
            _stopped.ProcessCancel();
            
            OnCommandCancel?.Invoke();
        }
    }
}