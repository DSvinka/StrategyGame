using Abstractions;
using Abstractions.Commands;

namespace Core.Commands
{
    public class AutoAttackCommand : IAttackCommand
    {
        public IAttackable Target { get; }

        public AutoAttackCommand(IAttackable target)
        {
            Target = target;
        }
    }
}