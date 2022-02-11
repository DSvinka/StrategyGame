using Abstractions;
using Abstractions.Commands;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {
        public override void Execute(IPatrolCommand command)
        {
            Debug.Log($"{name} Patrols!");
        }
    }
}