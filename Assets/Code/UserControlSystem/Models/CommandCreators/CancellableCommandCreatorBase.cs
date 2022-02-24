using System;
using System.Threading;
using Abstractions;
using Utils;
using Zenject;

namespace UserControlSystem.Models.CommandCreators
{
    public abstract class CancellableCommandCreatorBase<TCommand, TArgument> : CommandCreatorBase<TCommand>
    {
        [Inject] private AssetsContext _context;
        [Inject] private IAwaitable<TArgument> _awaitableArgument;

        private CancellationTokenSource _cancellationTokenSource;

        protected override async void SpecificCommand(Action<TCommand> creationCallback)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                var argument = await _awaitableArgument.WithCancellation(_cancellationTokenSource.Token);
                creationCallback?.Invoke(_context.Inject(CreateCommand(argument)));
            }
            catch
            {
                
            }
        }
        
        public override void ProcessCancel()
        {
            base.ProcessCancel();
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }

        protected abstract TCommand CreateCommand(TArgument argument);
    }
}