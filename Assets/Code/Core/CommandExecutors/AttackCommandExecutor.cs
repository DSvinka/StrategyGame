using Abstractions;
using Abstractions.Commands;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        public override void ExecuteSpecific(IAttackCommand command)
        {
            Debug.Log($"{name} Attacks!");
        }
    }
}