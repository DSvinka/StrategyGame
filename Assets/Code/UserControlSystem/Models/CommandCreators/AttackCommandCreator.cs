using Abstractions;
using Abstractions.Commands;
using Commands;

namespace UserControlSystem.Models.CommandCreators
{
    public sealed class AttackCommandCreator : CancellableCommandCreatorBase<IAttackCommand, IAttackable>
    {
        protected override IAttackCommand CreateCommand(IAttackable argument)
        {
            return new AttackCommand(argument);
        }
    }
}