using Abstractions;
using Abstractions.Commands;
using UnityEngine;
using Zenject;

namespace Core.Queues
{
    public sealed class MainBuildingCommandsQueue: MonoBehaviour, ICommandsQueue
    {
        [Inject] CommandExecutorBase<IProduceUnitCommand> _produceUnitCommandExecutor;
        [Inject] CommandExecutorBase<ISetRallyPointCommand> _setRallyPointCommandExecutor;
        
        public object CurrentCommand => default;
        
        public async void EnqueueCommand(object command)
        {
            await _produceUnitCommandExecutor.TryExecuteCommand(command);
            await _setRallyPointCommandExecutor.TryExecuteCommand(command);
        }
        
        public void Clear() { }
    }
}