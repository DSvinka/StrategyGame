using Abstractions;
using Abstractions.Commands;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        public override void Execute(IMoveCommand command)
        {
            Debug.Log($"{name} Moving!");
        }
    }
}