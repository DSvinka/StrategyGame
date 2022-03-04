using System.Threading.Tasks;
using Abstractions;
using Abstractions.Commands;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {
        public override Task ExecuteSpecific(IPatrolCommand command)
        {
            Debug.Log($"{name} is patrolling on {command.To}!");
            return Task.CompletedTask;
        }
    }
}