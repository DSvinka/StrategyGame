using System.Threading.Tasks;
using Abstractions;
using Abstractions.Commands;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        public override Task ExecuteSpecific(IAttackCommand command)
        {
            Debug.Log($"{name} attack {command.Target.GameObject.name}!");
            return Task.CompletedTask;
        }
    }
}