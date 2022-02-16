using Abstractions;
using Abstractions.Commands;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        public override void ExecuteSpecific(IMoveCommand command)
        {
            Debug.Log($"{name} is moving to {command.Target}!");
        }
    }
}