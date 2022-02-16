using Abstractions.Commands;
using UnityEngine;

namespace UserControlSystem.Commands
{
    public sealed class AttackCommand: IAttackCommand
    {
        public Transform Target { get; }

        public AttackCommand(Transform target)
        {
            Target = target;
        }
    }
}