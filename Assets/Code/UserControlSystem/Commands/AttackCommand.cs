using Abstractions;
using Abstractions.Commands;
using UnityEngine;

namespace UserControlSystem.Commands
{
    public sealed class AttackCommand: IAttackCommand
    {
        public IAttackable Target { get; }

        public AttackCommand(IAttackable target)
        {
            Target = target;
        }
    }
}