using Abstractions;
using Abstractions.Commands;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public override void Execute(IStopCommand command)
        {
            Debug.Log($"{name} Has Stopped!");
        }
    }
}