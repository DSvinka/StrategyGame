using Abstractions;
using Abstractions.Commands;

namespace Commands
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