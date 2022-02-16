using System;
using Abstractions.Commands;
using UserControlSystem.Commands;
using Utils;
using Zenject;

namespace UserControlSystem.Models.CommandCreators
{
    public sealed class AttackCommandCreator : CommandCreatorBase<IAttackCommand>
    {
        [Inject] private AssetsContext _context;

        protected override void SpecificCommand(Action<IAttackCommand> creationCallback)
        {
            creationCallback?.Invoke(_context.Inject(new AttackCommand()));
        }
    }
}