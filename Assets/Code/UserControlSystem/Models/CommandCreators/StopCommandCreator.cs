using System;
using Abstractions.Commands;
using Commands;
using Utils;
using Zenject;

namespace UserControlSystem.Models.CommandCreators
{
    public sealed class StopCommandCreator : CommandCreatorBase<IStopCommand>
    {
        [Inject] private AssetsContext _context;

        protected override void SpecificCommand(Action<IStopCommand> creationCallback)
        {
            creationCallback?.Invoke(_context.Inject(new StopCommand()));
        }
    }
}